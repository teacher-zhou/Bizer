using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bizer.Client;
internal class HttpClientInterceptor<TService> : IAsyncInterceptor where TService : class
{
    public HttpClientInterceptor(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    protected ILoggerFactory? LoggerFactory => ServiceProvider.GetService<ILoggerFactory>();

    public ILogger? Logger => LoggerFactory?.CreateLogger("DynamicHttpProxy");

    protected IHttpRemotingResolver Converter => ServiceProvider.GetRequiredService<IHttpRemotingResolver>();

    public IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();


    public HttpClientOptions Options => ServiceProvider.GetRequiredService<IOptions<HttpClientOptions>>().Value;

    public void InterceptAsynchronous(IInvocation invocation)
    {
        var response = Send(invocation, out var configuration);
        var stream = configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if ( stream != null && stream.Length>0 )
        {

            invocation.ReturnValue = JsonSerializer.DeserializeAsync(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        else
        {
            invocation.ReturnValue = Task.CompletedTask;
        }
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        var response = Send(invocation, out var configuration);
        var stream = configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if ( stream != null && stream.Length > 0 )
        {
            invocation.ReturnValue = JsonSerializer.DeserializeAsync<TResult?>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        else
        {
            invocation.ReturnValue = Task.FromResult<TResult>(default);
        }
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        var response = Send(invocation, out var configuration);
        var stream = configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if ( stream is not null && stream.Length > 0 )
        {
            invocation.ReturnValue = JsonSerializer.Deserialize(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        //invocation.ReturnValue = HandleResponse(response, res =>
        //{
        //    var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //    if ( string.IsNullOrEmpty(content) )
        //    {
        //        return default;
        //    }

        //    return JsonSerializer.Deserialize(content, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //});
    }




    //async Task<TResult?> SendAsync<TResult>(HttpRequestMessage request)
    //{
    //    using HttpClient client = CreateClient(out var configuration);

    //    var response = await client.SendAsync(request);

    //    response.EnsureSuccessStatusCode();
    //    Logger?.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");
    //    return (TResult?)await configuration.ResponseHandler(response);
    //}
    HttpResponseMessage Send(IInvocation invocation,out HttpClientConfiguration configuration)
    {
        using HttpClient client = CreateClient(out configuration);
        var request = CreateRequestMessage(invocation);
        var response = client.Send(request);
        Logger?.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");
        return response;
    }

    private HttpClient CreateClient(out HttpClientConfiguration configuration)
    {
        var serviceType = typeof(TService);
        configuration = Options.HttpConfigurations[serviceType];
        var client = HttpClientFactory.CreateClient(configuration.Name);
        return client;
    }

    HttpRequestMessage CreateRequestMessage(IInvocation invocation)
    {
        if ( invocation is null )
        {
            throw new ArgumentNullException(nameof(invocation));
        }

        var request = new HttpRequestMessage
        {
            Method = Converter.GetHttpMethod(invocation.Method)
        };

        var pathBuilder = new StringBuilder();
        var apiRoute = Converter.GetApiRoute(typeof(TService), invocation.Method); 

        if ( !apiRoute.StartsWith("/") )
        {
            pathBuilder.Append('/');
        }
        pathBuilder.Append(apiRoute);

        var queryParameters = new List<string>();
        var parameters = Converter.GetParameters(invocation.Method);
        var key = DefaultHttpRemotingResolver.GetMethodCacheKey(invocation.Method);
        var parameterInfoList = parameters[key];

        foreach ( var param in parameterInfoList )
        {            
            var name = param.GetParameterNameInHttpRequest();
            var value = param.Value?.ToString() ?? invocation.GetArgumentValue(param.Position)?.ToString() ?? string.Empty;

            switch ( param.Type )
            {
                case HttpParameterType.FromBody:
                    var json = JsonSerializer.Serialize(value);
                    request.Content = new StringContent(json, Encoding.Default, "application/json");
                    break;
                case HttpParameterType.FromHeader:
                    request.Headers.Add(name, value);
                    break;
                case HttpParameterType.FromForm:
                    var arguments = new Dictionary<string, string>();
                    if ( param.ValueType != typeof(string) && param.ValueType.IsClass )
                    {
                        param.ValueType.GetProperties().ForEach(property =>
                        {
                            if ( property.CanRead )
                            {
                                name = property.Name;
                                value = property.GetValue(value)?.ToString();
                            }
                        });

                    }
                    else
                    {
                        arguments.Add(name!, value);
                    }
                    request.Content = new FormUrlEncodedContent(arguments);
                    break;
                case HttpParameterType.FromPath://路由替换
                    var match = Regex.Match(pathBuilder.ToString(), @"{\w+}");
                    if ( match.Success )
                    {
                        pathBuilder.Replace(match.Value, match.Result(value));
                    }
                    break;
                case HttpParameterType.FromQuery:
                    if (param.ValueType!=typeof(string) && param.ValueType.IsClass )
                    {
                        foreach ( var property in param.ValueType.GetProperties() )
                        {
                            if ( !property.CanRead )
                            {
                                continue;
                            }

                            if ( property.TryGetCustomAttribute<JsonPropertyNameAttribute>(out var jsonNameProperty) )
                            {
                                name = jsonNameProperty!.Name;
                            }
                            else
                            {
                                name = property.Name;
                            }

                            var propertyValue = property.GetValue(value);
                            if ( propertyValue is not null )
                            {
                                queryParameters.Add($"{name}={propertyValue}");
                            }
                        }
                    }
                    else
                    {
                        queryParameters.Add($"{name}={value}");
                    }
                    break;
                default:
                    break;
            }
        }

        var uriString = $"{pathBuilder}{(queryParameters.Count > 0 ? $"?{string.Join("&", queryParameters)}" : String.Empty)}";


        request.RequestUri = new(uriString, UriKind.Relative);
        Logger?.LogDebug($"请求的 uri 资源路径：{uriString}");
        return request;
    }


}
