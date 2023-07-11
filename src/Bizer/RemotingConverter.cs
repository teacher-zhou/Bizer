using System.Reflection;

namespace Bizer;
public class RemotingConverter : IRemotingConverter
{
    public virtual bool CanApiExplore(Type interfaceType)
    => interfaceType.IsDefined(typeof(ApiRouteAttribute));

    public virtual bool CanApiExplore(MethodInfo? method) => method is not null && method.IsDefined(typeof(HttpMethodAttribute));

    public virtual string GetApiRoute(Type interfaceType, MethodInfo method)
    {
        if (!CanApiExplore(interfaceType) )
        {
            throw new InvalidOperationException($"{interfaceType.Name}必须标记{nameof(ApiRouteAttribute)}特性才可以识别为路由");
        }

        var appenderList = new List<string?>();

        var apiRoute = interfaceType.GetCustomAttribute<ApiRouteAttribute>();

        appenderList.Add(apiRoute!.Template);

        if ( !method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute) )
        {
            throw new InvalidOperationException($"{method.Name}必须标记{nameof(HttpMethodAttribute)}特性");
        }

        appenderList.Add(httpMethodAttribute!.Template);

        return string.Join("/", appenderList);
    }

    public HttpMethod GetHttpMethod(MethodInfo method)
    {
        if ( !method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute) )
        {
            throw new InvalidOperationException($"{method.Name}必须标记{nameof(HttpMethodAttribute)}特性");
        }

        return httpMethodAttribute!.Method;
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


    public static string GetMethodCacheKey(MethodInfo method)
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
