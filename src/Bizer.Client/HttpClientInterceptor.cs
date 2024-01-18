using System.Text;
using System.Text.Json;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

    public HttpClientOptions Options => ServiceProvider.GetRequiredService<IOptions<HttpClientOptions>>().Value;

    public IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();

    public void InterceptAsynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = SendAsync(invocation); //处理 Task
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = SendAsync<TResult>(invocation);//处理 Task<TResult>
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        Send(invocation); //同步
    }


    void Send(IInvocation invocation)
    {
        using HttpClient client = CreateClient(out var configuration);
        var request = CreateRequestMessage(invocation);

        var response = client.Send(request);

        var stream = configuration.ResponseHandler(response).GetAwaiter().GetResult();
        if (stream is not null && stream.Length > 0)
        {
            invocation.ReturnValue = JsonSerializer.Deserialize(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }


    async Task<object?> SendAsync(IInvocation invocation)
    {
        using HttpClient client = CreateClient(out var configuration);
        var request = CreateRequestMessage(invocation);

        var response = await client.SendAsync(request);
        var stream = await configuration.ResponseHandler(response);
        if (stream is not null && stream.Length > 0)
        {
            return await JsonSerializer.DeserializeAsync(stream, invocation.Method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        return default;
    }

    async Task<TResult?> SendAsync<TResult>(IInvocation invocation)
    {
        using HttpClient client = CreateClient(out var configuration);
        var request = CreateRequestMessage(invocation);

        var response = await client.SendAsync(request);

        var stream = await configuration.ResponseHandler(response);
        if (stream is not null && stream.Length > 0)
        {
            return await JsonSerializer.DeserializeAsync<TResult?>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).AsTask();
        }
        return default;
    }

    private HttpClient CreateClient(out HttpClientConfiguration configuration)
    {
        var serviceType = typeof(TService);
        configuration = Options.HttpConfigurations[serviceType];
        var client = HttpClientFactory.CreateClient(configuration.Name);
        return client;
    }

    //HttpRequestMessage CreateRequestMessage(MethodInfo method, object[] args)
    HttpRequestMessage CreateRequestMessage(IInvocation invocation)
    {
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
            var value = invocation.Arguments[param.Position] ?? default;

            switch (param.Type)
            {
                case HttpParameterType.FromBody:
                    var json = JsonSerializer.Serialize(value);
                    request.Content = new StringContent(json, Encoding.Default, "application/json");
                    break;
                case HttpParameterType.FromHeader:
                    request.Headers.Add(name, value?.ToString());
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
                        arguments.Add(name!, value?.ToString());
                    }
                    request.Content = new FormUrlEncodedContent(arguments);
                    break;
                case HttpParameterType.FromPath://路由替换
                    var match = Regex.Match(pathBuilder.ToString(), @"{\w+}");
                    if (match.Success)
                    {
                        pathBuilder.Replace(match.Value, match.Result(value?.ToString()));
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

                            if (property.TryGetCustomAttribute<JsonIgnoreAttribute>(out _))
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

        Logger?.LogDebug(new EventId(9500), $"[Request URI]: {request.RequestUri}");
        Logger?.LogDebug(new EventId(9501), $"[Request Method]: {request.Method}");
        return request;
    }
}
