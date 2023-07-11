using Bizer;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerDependencyInjectionExtensions
{
    /// <summary>
    /// 添加自动发现服务。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">配置自动发现的委托。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="configure"/> 是 <c>null</c>。</exception>
    public static IServiceCollection AddAutoDiscoveryOptions(this IServiceCollection services, Action<AutoDiscoveryOptions> configure)
    {
        if ( configure is null )
        {
            throw new ArgumentNullException(nameof(configure));
        }
        var options = new AutoDiscoveryOptions();
        configure(options);
        services.AddSingleton(options);

        services.AddInjectServices(options.GetMatchesAssemblies().ToArray());
        return services;
    }
    /// <summary>
     /// 自动注入接口标记了 <see cref="ServiceAttribute"/> 类。该接口将成为服务，对应的类则成为实例。
     /// </summary>
     /// <param name="services"></param>
     /// <param name="assemblies">应用程序集。</param>
    static IServiceCollection AddInjectServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        if ( assemblies is null )
        {
            throw new ArgumentNullException(nameof(assemblies));
        }
        //找出接口标记了 ServiceAttribute 特性的类

        var allClassTypes = assemblies.SelectMany(m => m.ExportedTypes).Where(m => m.IsClass && !m.IsAbstract)
            .Where(classType => classType.GetInterfaces().Any(interfaceType => interfaceType.IsDefined(typeof(ServiceAttribute))));


        foreach ( var implementType in allClassTypes )
        {
            var serviceAttributeInterface = implementType.GetInterfaces().Where(interfaceType => interfaceType.IsDefined(typeof(ServiceAttribute))).Last();

            var serviceAttribute = serviceAttributeInterface.GetCustomAttribute<ServiceAttribute>();

            services.Add(new(serviceAttributeInterface, implementType, serviceAttribute!.Lifetime));
        }
        return services;
    }
}