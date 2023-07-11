using System.Net.Http;
using System.Reflection;

namespace Bizer;
/// <summary>
/// 提供对远程服务转换的功能。
/// </summary>
public interface IRemotingConverter
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
    Dictionary<string, (HttpParameterType type, string parameterName, object? parameterValue)> GetParameters(MethodInfo method);
}
