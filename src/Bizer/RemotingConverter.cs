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

    public Dictionary<string, IEnumerable<HttpParameterInfo>> GetParameters(MethodInfo method)
    {
        method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute);

        var dic = new Dictionary<string, IEnumerable<HttpParameterInfo>>();

        string key = GetMethodCacheKey(method);

        var parameterInfoList = new List<HttpParameterInfo>();

        var parameters = method.GetParameters();

        foreach ( var param in parameters )
        {
            var parameterInfo = new HttpParameterInfo
            {
                Name = param.Name,
                Position = param.Position,
                ValueType = param.ParameterType
            };

            if ( param.TryGetCustomAttribute<HttpParameterAttribute>(out var parameterAttribute) )
            {
                parameterInfo.Type = parameterAttribute!.Type;
                parameterInfo.Alias = parameterAttribute?.Name;
            }
            else
            {
                parameterInfo.Type = httpMethodAttribute?.Method.Method switch
                {
                    "POST" => HttpParameterType.FromBody,
                    _ => HttpParameterType.FromQuery,
                };
            }

            parameterInfoList.Add(parameterInfo);
        }

        dic[key] = parameterInfoList;

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
