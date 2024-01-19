namespace Bizer;

/// <summary>
/// 当前接口作为注入的服务，并且实现该接口的类（非抽象类或泛型类）会作为该服务的实现。
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class InjectServiceAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="InjectServiceAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="lifetime">生命周期。</param>
    public InjectServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) => Lifetime = lifetime;
    /// <summary>
    /// 获取生命周期。
    /// </summary>
    public ServiceLifetime Lifetime { get; }
}
