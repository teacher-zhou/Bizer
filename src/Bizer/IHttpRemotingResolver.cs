using System.Reflection;

namespace Bizer;
/// <summary>
/// 提供对 HTTP 远程访问的解析器。
/// </summary>
public interface IHttpRemotingResolver
{
    /// <summary>
    /// 能否被 api 发现。
    /// </summary>
    /// <param name="interfaceType">当前接口。</param>
    /// <returns></returns>
    bool CanApiExplore(Type interfaceType);

    /// <summary>
    /// 能否被 api 发现。
    /// </summary>
    /// <param name="method">运行的方法。</param>
    /// <returns></returns>
    bool CanApiExplore(MethodInfo? method);
    /// <summary>
    /// 获取 api 路由。
    /// </summary>
    /// <param name="interfaceType">接口的类型。</param>
    /// <param name="method">正在执行的方法。</param>
    /// <returns></returns>
    string GetApiRoute(Type interfaceType,MethodInfo method);
    /// <summary>
    /// 从接口方法中获取 <see cref="HttpMethod"/> 。
    /// </summary>
    /// <param name="method">当前的方法。</param>
    /// <returns></returns>
    HttpMethod GetHttpMethod(MethodInfo method);
    /// <summary>
    /// 获取方法的参数。
    /// </summary>
    /// <param name="method">方法对象。</param>
    /// <returns></returns>
    Dictionary<string, IEnumerable<HttpParameterInfo>> GetParameters(MethodInfo method);
}

/// <summary>
/// HTTP 参数信息。
/// </summary>
public class HttpParameterInfo
{
    /// <summary>
    /// 参数类型。
    /// </summary>
    public HttpParameterType Type { get; set; } = HttpParameterType.FromQuery;
    /// <summary>
    /// 方法的参数名称。
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 参数的别名。
    /// </summary>
    public string? Alias { get; set; }
    /// <summary>
    /// 参数值。
    /// </summary>
    public object? Value { get; set; }
    /// <summary>
    /// 参数值的数据类型。
    /// </summary>
    public Type? ValueType { get; set; }

    /// <summary>
    /// 参数在方法里的位置。
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// 获取参数在 HTTP 请求时的名称。
    /// </summary>
    /// <returns></returns>
    public string? GetParameterNameInHttpRequest() => Alias ?? Name;
}
