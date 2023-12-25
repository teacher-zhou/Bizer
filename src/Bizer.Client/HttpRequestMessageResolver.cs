using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Client;

public class HttpRequestMessageResolver
{
    public HttpRequestMessageResolver(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IHttpRemotingResolver Converter => ServiceProvider.GetRequiredService<IHttpRemotingResolver>();
    public IServiceProvider ServiceProvider { get; }

    public HttpRequestMessage Resolve<TService>(MethodInfo method)
    {
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
            var value = param.Value?.ToString(); // ?? invocation.GetArgumentValue(param.Position)?.ToString() ?? string.Empty;

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
        return request;
    }

    public HttpRequestMessage Resolve<TService>(string methodName)
    {
        var methodInfo = typeof(TService).GetMethod(methodName) ?? throw new InvalidOperationException($"No {methodName} method for {typeof(TService).Name}");
        return Resolve<TService>(methodInfo);
    }
}
