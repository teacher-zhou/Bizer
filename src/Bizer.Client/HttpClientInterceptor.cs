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
        invocation.ReturnValue = SendAsync(CreateRequestMessage(invocation));
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = SendAsync<TResult>(CreateRequestMessage(invocation));
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        using HttpClient client = CreateClient();
        var request = CreateRequestMessage(invocation);
        var response = client.Send(request);

        invocation.ReturnValue = HandleResponse(response, res =>
        {
            var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if ( string.IsNullOrEmpty(content) )
            {
                return default;
            }

            return JsonSerializer.Deserialize(content, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        });
    }
    async Task<TResult?> SendAsync<TResult>(HttpRequestMessage request)
    {
        using HttpClient client = CreateClient();

        var response = await client.SendAsync(request);

        return HandleResponse(response, res =>
        {
            var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if ( string.IsNullOrEmpty(content) )
            {
                return default;
            }

            return JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        });
    }
    async Task SendAsync(HttpRequestMessage request)
    {
        using HttpClient client = CreateClient();
        var response = await client.SendAsync(request);
        await HandleResponse(response, _ => Task.CompletedTask);
    }

    TResult? HandleResponse<TResult>(HttpResponseMessage response,Func<HttpResponseMessage,TResult>? handler=default)
    {
        response.EnsureSuccessStatusCode();
        Logger?.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");
        if ( handler is not null )
        {
            return handler.Invoke(response);
        }
        return default;
    }

    private HttpClient CreateClient()
    {
        var serviceType = typeof(TService);
        var configuration = Options.HttpConfigurations[serviceType];
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
            var value = param.Value?.ToString() ?? invocation.GetArgumentValue(param.Position)?.ToString();

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
