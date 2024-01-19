using System.Reflection;

namespace Bizer;

/// <summary>
/// 默认的 HTTP 远程解析器。
/// </summary>
public class DefaultHttpRemotingResolver : IHttpRemotingResolver
{
    /// <summary>
    /// 定义了 <see cref="ApiRouteAttribute"/> 特性的接口才能作为服务 API 被发现。
    /// </summary>
    /// <param name="interfaceType">接口类型。</param>
    /// <returns>能被发现，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public virtual bool CanApiExplore(Type interfaceType) => interfaceType.IsDefined(typeof(ApiRouteAttribute));

    /// <summary>
    /// 定义了 <see cref="HttpMethodAttribute"/> 特性的接口才能作为服务 API 被发现。
    /// </summary>
    /// <param name="method">运行的方法。</param>
    /// <returns></returns>
    public virtual bool CanApiExplore(MethodInfo? method) => method is not null && method.IsDefined(typeof(HttpMethodAttribute));

    /// <summary>
    /// 获取接口定义的 <see cref="ApiRouteAttribute.Template"/> 和 <see cref="HttpMethodAttribute.Template"/> 作为路由字符串。
    /// </summary>
    /// <param name="interfaceType">接口的类型。</param>
    /// <param name="method">正在执行的方法。</param>
    /// <returns></returns>
    public virtual string GetApiRoute(Type interfaceType, MethodInfo method)
    {
        if (!CanApiExplore(interfaceType))
        {
            throw new InvalidOperationException($"{interfaceType.Name}必须标记{nameof(ApiRouteAttribute)}特性才可以识别为路由");
        }

        var appenderList = new List<string?>();

        var apiRoute = interfaceType.GetCustomAttribute<ApiRouteAttribute>();

        appenderList.Add(apiRoute!.Template);

        if (!method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute))
        {
            throw new InvalidOperationException($"{method.Name}必须标记{nameof(HttpMethodAttribute)}特性");
        }

        appenderList.Add(httpMethodAttribute!.Template);

        return string.Join("/", appenderList);
    }

    /// <summary>
    /// 从方法中获取 <see cref="HttpMethod"/> 。
    /// </summary>
    /// <param name="method">当前的方法。</param>
    /// <returns></returns>
    public HttpMethod GetHttpMethod(MethodInfo method)
    {
        if (!method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute))
        {
            throw new InvalidOperationException($"{method.Name}必须标记{nameof(HttpMethodAttribute)}特性");
        }

        return httpMethodAttribute!.Method;
    }

    /// <summary>
    /// 获取方法的参数。
    /// </summary>
    /// <param name="method">方法对象。</param>
    /// <returns></returns>
    public Dictionary<string, IEnumerable<HttpParameterInfo>> GetParameters(MethodInfo method)
    {
        method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute);

        var dic = new Dictionary<string, IEnumerable<HttpParameterInfo>>();

        string key = GetMethodCacheKey(method);

        var parameterInfoList = new List<HttpParameterInfo>();

        var parameters = method.GetParameters();

        foreach (var param in parameters)
        {
            var parameterInfo = new HttpParameterInfo
            {
                Name = param.Name,
                Position = param.Position,
                ValueType = param.ParameterType
            };

            if (param.TryGetCustomAttribute<HttpParameterAttribute>(out var parameterAttribute))
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
        var list = new List<string>()
        {
            method.ReflectedType!.Name,
            method.Name
        };

        list.AddRange(method.GetParameters().Select(p => $"{p.ParameterType.Name}_{p.Name}"));
        return string.Join(":", list);
    }
}
