using Bizer.Client.Proxy;

using Castle.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bizer.Client;
internal class HttpClientInterceptor<TService> : IAsyncInterceptor where TService : class
{
    private readonly HttpClient _client;

    public HttpClientInterceptor(IServiceProvider serviceProvider, HttpClient client)
    {
        ServiceProvider = serviceProvider;
        this._client = client;
    }

    public IServiceProvider ServiceProvider { get; }

    protected ILoggerFactory? LoggerFactory => ServiceProvider.GetService<ILoggerFactory>();

    public ILogger? Logger => LoggerFactory?.CreateLogger("DynamicHttpProxy");

    protected IHttpRemotingResolver Converter => ServiceProvider.GetRequiredService<IHttpRemotingResolver>();

    public HttpClientOptions Options => ServiceProvider.GetRequiredService<IOptions<HttpClientOptions>>().Value;

    public HttpRequestMessageResolver HttpRequestMessageResolver => ServiceProvider.GetRequiredService<HttpRequestMessageResolver>();

    public HttpClientConfiguration Configuration => new();

    public async void InterceptAsynchronous(IInvocation invocation)
    {
        var response = await Send(invocation);
        var stream = Configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if (stream != null && stream.Length > 0)
        {

            invocation.ReturnValue = JsonSerializer.DeserializeAsync(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        else
        {
            invocation.ReturnValue = Task.CompletedTask;
        }
    }

    public async void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        var response = await Send(invocation);
        var stream = Configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if (stream != null && stream.Length > 0)
        {
            invocation.ReturnValue = JsonSerializer.DeserializeAsync<TResult?>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        else
        {
            invocation.ReturnValue = Task.FromResult<TResult>(default);
        }
    }

    public async void InterceptSynchronous(IInvocation invocation)
    {
        var response = await Send(invocation);
        var stream = Configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if (stream is not null && stream.Length > 0)
        {
            invocation.ReturnValue = JsonSerializer.Deserialize(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }


    async Task<HttpResponseMessage> Send(IInvocation invocation)
    {
        using HttpClient client = CreateClient();
        var request = CreateRequestMessage(invocation);
        var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        Logger?.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");
        return response;
    }
    //HttpResponseMessage Send(MethodInfo method, object[] parameters, out HttpClientConfiguration configuration)
    //{
    //    using HttpClient client = CreateClient(out configuration);
    //    var request = CreateRequestMessage(method, parameters);
    //    var response = client.Send(request);
    //    Logger?.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");
    //    return response;
    //}

    private HttpClient CreateClient()
    {
        //var serviceType = typeof(TService);
        //configuration = Options.HttpConfigurations[serviceType];
        //var client = HttpClientFactory.CreateClient(configuration.Name);
        //return client;

        return _client;
    }
    //HttpRequestMessage CreateRequestMessage(MethodInfo method, object[] args)
    HttpRequestMessage CreateRequestMessage(IInvocation invocation)
    {
        //if ( invocation is null )
        //{
        //    throw new ArgumentNullException(nameof(invocation));
        //}

        var method = invocation.Method;

        var request = new HttpRequestMessage
        {
            Method = Converter.GetHttpMethod(method)
        };

        var pathBuilder = new StringBuilder();
        var apiRoute = Converter.GetApiRoute(typeof(TService), method);

        if (!apiRoute.StartsWith("/"))
        {
            pathBuilder.Append('/');
        }
        pathBuilder.Append(apiRoute);

        var queryParameters = new List<string>();
        var parameters = Converter.GetParameters(method);
        var key = DefaultHttpRemotingResolver.GetMethodCacheKey(method);
        var parameterInfoList = parameters[key];

        foreach (var param in parameterInfoList)
        {
            var name = param.GetParameterNameInHttpRequest();
            var value = invocation.GetArgumentValue(param.Position)?.ToString() ?? string.Empty;

            switch (param.Type)
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
                    if (param.ValueType != typeof(string) && param.ValueType.IsClass)
                    {
                        param.ValueType.GetProperties().ForEach(property =>
                        {
                            if (property.CanRead)
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
                    if (match.Success)
                    {
                        pathBuilder.Replace(match.Value, match.Result(value));
                    }
                    break;
                case HttpParameterType.FromQuery:
                    if (param.ValueType != typeof(string) && param.ValueType.IsClass)
                    {
                        foreach (var property in param.ValueType.GetProperties())
                        {
                            if (!property.CanRead)
                            {
                                continue;
                            }

                            if (property.TryGetCustomAttribute<JsonPropertyNameAttribute>(out var jsonNameProperty))
                            {
                                name = jsonNameProperty!.Name;
                            }
                            else
                            {
                                name = property.Name;
                            }

                            var propertyValue = property.GetValue(value);
                            if (propertyValue is not null)
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
