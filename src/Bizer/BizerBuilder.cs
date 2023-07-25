using Bizer.Security;
using System.Reflection;

namespace Bizer;

/// <summary>
/// Bizer 框架的构造器。
/// </summary>
public class BizerBuilder
{
    /// <summary>
    /// 初始化 <see cref="BizerBuilder"/> 类的新实例。
    /// </summary>
    /// <param name="services">服务集合。</param>
    internal BizerBuilder(IServiceCollection services) => Services = services;

    /// <summary>
    /// 获取服务集合。
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// 获取自动发现的配置。
    /// </summary>
    public AutoDiscoveryOptions AutoDiscovery { get; private set; } = new();

    /// <summary>
    /// 添加自动发现。
    /// </summary>
    /// <param name="configure">一个配置自动发现的委托。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="configure"/> 是 <c>null</c>。</exception>
    internal BizerBuilder AddAutoDiscovery(Action<AutoDiscoveryOptions>? configure=default)
    {
        configure?.Invoke(AutoDiscovery);
        Services.AddSingleton(AutoDiscovery);
        return this;
    }

    /// <summary>
    /// 添加指定的 HTTP 远程解析器。
    /// </summary>
    /// <typeparam name="TConverter">转换器类型。</typeparam>
    public BizerBuilder AddHttpRemotingResolver<TConverter>() where TConverter : class, IHttpRemotingResolver
    {
        Services.AddSingleton<IHttpRemotingResolver, TConverter>();
        return this;
    }

    /// <summary>
    /// 添加默认的 HTTP 远程解析器。
    /// </summary>
    public BizerBuilder AddHttpRemotingResolver() => AddHttpRemotingResolver<DefaultHttpRemotingResolver>();

    /// <summary>
    /// 添加自动注入功能。
    /// 只有添加了特性 <see cref="InjectServiceAttribute"/> 的接口，会自动将实现类作为进行注册。
    /// </summary>
    /// <returns></returns>
    public BizerBuilder AddServiceInjection()
    {
        var allClassTypes = AutoDiscovery.GetDiscoveredAssemblies().SelectMany(m => m.ExportedTypes).Where(m => m.IsClass && !m.IsAbstract)
           .Where(classType => classType.GetInterfaces().Any(interfaceType => interfaceType.IsDefined(typeof(InjectServiceAttribute))));

        foreach ( var implementType in allClassTypes )
        {
            var serviceAttributeInterface = implementType.GetInterfaces().Where(interfaceType => interfaceType.IsDefined(typeof(InjectServiceAttribute))).Last();

            var serviceAttribute = serviceAttributeInterface.GetCustomAttribute<InjectServiceAttribute>();

            Services.Add(new(serviceAttributeInterface, implementType, serviceAttribute!.Lifetime));
        }
        return this;
    }

    /// <summary>
    /// 添加主体访问器的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TCurrentPrincipalAccessor">主体访问器类型。</typeparam>
    public BizerBuilder AddCurrentPrincipalAccessor<TCurrentPrincipalAccessor>()
        where TCurrentPrincipalAccessor : class, ICurrentPrincipalAccessor
    {
        Services.AddTransient<ICurrentPrincipalAccessor, TCurrentPrincipalAccessor>();
        return this;
    }

}
