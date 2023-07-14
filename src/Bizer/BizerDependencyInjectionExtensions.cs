using Bizer;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerDependencyInjectionExtensions
{
    /// <summary>
    /// 添加 Bizer 服务并配置自动发现服务。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">一个进行自动发现的配置委托。</param>
    /// <returns></returns>
    public static BizerBuilder AddBizer(this IServiceCollection services, Action<AutoDiscoveryOptions>? configure = default)
    {
        var builder = new BizerBuilder(services);

        if ( configure is not null )
        {
            builder.AddAutoDiscovery(configure);
        }
        return builder;
    }

}