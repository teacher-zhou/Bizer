using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;
/// <summary>
/// <see cref="IServiceCollection"/> 的扩展。
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 获取指定服务的实例。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    /// <returns>指定服务的实例或 null。</returns>
    public static TService? GetServiceInstance<TService>(this IServiceCollection services) where TService : class
        => (TService?)services.FirstOrDefault(m => m.ServiceType == typeof(TService))?.ImplementationInstance;

    /// <summary>
    /// 获取 <see cref="IConfiguration"/> 服务。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    /// <returns><see cref="IConfiguration"/> 服务的实例或 null。</returns>
    public static IConfiguration? GetConfiguration(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetServiceInstance<HostBuilderContext>();
        if (hostBuilderContext?.Configuration != null)
        {
            return hostBuilderContext.Configuration as IConfigurationRoot;
        }

        return services.GetServiceInstance<IConfiguration>();
    }

    /// <summary>
    /// 尝试获取指定的服务并返回布尔值。
    /// </summary>
    /// <typeparam name="TService">服务的类型。</typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="service">若返回结果是 <c>true</c>，则返回指定的服务类型的实例；否则为 <c>null</c>。</param>
    /// <returns>已添加服务，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool TryGetService<TService>(this IServiceProvider serviceProvider, out TService? service) where TService : class
    {
        service = serviceProvider.GetService<TService>();
        return service is not null;
    }
}
