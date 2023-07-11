using Microsoft.Extensions.DependencyInjection;

namespace Bizer;

/// <summary>
/// 定义当前接口作为注入的服务。
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class ServiceAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="ServiceAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="lifetime">生命周期。</param>
    public ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
    /// <summary>
    /// 获取生命周期。
    /// </summary>
    public ServiceLifetime Lifetime { get; }
}
