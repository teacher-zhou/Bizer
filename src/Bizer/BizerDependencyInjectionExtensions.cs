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

        return services;
    }

    public static IServiceCollection AddRemotingConverter(this IServiceCollection services)
        => services.AddSingleton<IRemotingConverter, RemotingConverter>();
}