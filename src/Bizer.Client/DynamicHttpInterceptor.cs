using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bizer.Client;
internal class DynamicHttpInterceptor<TService> : IAsyncInterceptor where TService : class
{
    public DynamicHttpInterceptor(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    public IHttpClientFactory HttpClientFactory { get; }

    protected ILoggerFactory? LoggerFactory => ServiceProvider.GetService<ILoggerFactory>();

    public ILogger? Logger => LoggerFactory?.CreateLogger("DynamicHttpProxy");

    protected IRemotingConverter Converter => ServiceProvider.GetRequiredService<IRemotingConverter>();

    DynamicHttpClientProxy<TService> DynamicHttpClientProxy => (DynamicHttpClientProxy<TService>)ServiceProvider.GetRequiredService(typeof(DynamicHttpClientProxy<>).MakeGenericType(typeof(TService)));

    public void InterceptAsynchronous(IInvocation invocation)
    {
       var result= DynamicHttpClientProxy.SendAsync(CreateRequestMessage(invocation));
        invocation.ReturnValue = result.GetAwaiter().GetResult();
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        throw new NotImplementedException();
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        var result = DynamicHttpClientProxy.SendAsync(CreateRequestMessage(invocation));
        invocation.ReturnValue = result.Result;
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
        var key = RemotingConverter.GetMethodCacheKey(invocation.Method);
        var parameterInfoList = parameters[key];

        foreach ( var param in parameterInfoList )
        {
            var name = param.Name;
            var value = param.Value?.ToString() ?? string.Empty;

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
        Logger?.LogDebug($"请求的 uri 资源路径：{request.RequestUri}");
        return request;
    }


}
