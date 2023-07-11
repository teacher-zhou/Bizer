using System.Net.Http;
using System.Reflection;

namespace Bizer.AspNetCore;

/// <summary>
/// Api 转换器。
/// </summary>
internal class ApiConverter : IRemotingConverter
{
    private readonly BizerApiOptions _apiOptions;

    public ApiConverter(BizerApiOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }

    public bool CanApiExplorer(MethodInfo method)
    {
        return method is not null && method.TryGetCustomAttribute<HttpMethodAttribute>(out _);
    }

    public string GetApiRoute(Type interfaceType, string methodName, HttpMethodAttribute? httpMethodAttribute)
    {
        if ( interfaceType is null )
        {
            throw new ArgumentNullException(nameof(interfaceType));
        }

        var routeAppender = new List<string>();

        if ( interfaceType.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute) )
        {
            routeAppender.Add(routeAttribute!.Template);
        }
        else
        {
            var name = interfaceType.Name;

            _apiOptions.ControllerPrefixKeywords.Where(suffix => name.EndsWith(suffix))
                .ForEach(text =>
                {
                    name = name.Replace(text, "");
                });
            routeAppender.Add(name);
        }

        if ( string.IsNullOrWhiteSpace(httpMethodAttribute!.Template) )
        {
            var actionName = methodName;

            if ( actionName.EndsWith("Async") )
            {
                actionName = actionName.Replace("Async", string.Empty);
            }

            foreach ( var trimPrefix in _apiOptions.ActionSuffixKeywords.Where(trimPrefix => actionName.StartsWith(trimPrefix)) )
            {
                actionName = actionName[trimPrefix.Length..];
                break;
            }
            routeAppender.Add(actionName);
        }
        else
        {
            routeAppender.Add(httpMethodAttribute!.Template);
        }
        return string.Join("/", routeAppender);
    }

    public HttpMethod GetHttpMethod(string actionName, MethodInfo method)
    {
        if(method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute) )
        {
            return httpMethodAttribute.Method;
        }

        foreach ( var item in _apiOptions.HttpMethodSuffixMapping )
        {
            if ( actionName.StartsWith(item.Key) )
            {
                return item.Value;
            }
        }

        throw new InvalidOperationException($"没有找到对应的 HttpMethod 方式，请在方法上定义{nameof(HttpMethodAttribute)}特性");
    }

    public Dictionary<string, (HttpParameterType type, string parameterName, object? parameterValue)> GetParameters(MethodInfo method)
    {
        method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute);

        var dic = new Dictionary<string, (HttpParameterType type, string parameterName, object? parameterValue)>();

        string key = GetMethodCacheKey(method);

        var parameters = method.GetParameters();

        foreach ( var param in parameters )
        {
            HttpParameterType type;
            string name = method.Name;
            if ( param.TryGetCustomAttribute<HttpParameterAttribute>(out var parameterAttribute) )
            {
                type = parameterAttribute!.Type;
                name = parameterAttribute!.Name!;
            }
            else
            {
                type = httpMethodAttribute?.Method.Method switch
                {
                    "POST" => HttpParameterType.FromBody,
                    _ => HttpParameterType.FromQuery,
                };
            }

            dic[key] = new(type, name, default);
        }

        return dic;
    }

    internal static string GetMethodCacheKey(MethodInfo method)
    {
        var list = new List<string?>()
        {
            method?.ReflectedType?.Name,
            method?.Name
        };

        list.AddRange(method.GetParameters().Select(p => $"{p.ParameterType.Name}_{p.Name}"));
        return string.Join(":", list);
    }
}
