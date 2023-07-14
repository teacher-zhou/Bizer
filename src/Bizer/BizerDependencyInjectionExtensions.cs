using Bizer;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerDependencyInjectionExtensions
{
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