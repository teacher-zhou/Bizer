using Bizer;
using Bizer.Security;
using System.Reflection;

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

            builder.AddAutoDiscovery(configure);
        return builder;
    }
    /// <summary>
    /// 添加当前线程作为主体的访问器。
    /// </summary>
    /// <param name="builder"></param>
    public static BizerBuilder AddThreadCurrentPrincipalAccessor(this BizerBuilder builder)
        => builder.AddCurrentPrincipalAccessor<ThreadCurrentPrincipalAccessor>();


    /// <summary>
    /// 添加模块化的功能。可实现 <see cref="AppModule"/> 以支持模块化。
    /// </summary>
    /// <param name="exclude">排除模块化的程序集数组。</param>
    /// <returns></returns>
    public static BizerBuilder AddModular(this BizerBuilder builder)
    {
        var allPublicTypes = builder.AutoDiscovery.GetDiscoveredAssemblies().SelectMany(m => m.ExportedTypes);

        var moduleTypes = allPublicTypes.Where(instanceType => instanceType.IsClass && !instanceType.IsAbstract && typeof(AppModule).IsAssignableFrom(instanceType));

        LookupModules(moduleTypes);

        var provider = builder.Services.BuildServiceProvider();

        foreach (var type in moduleTypes )
        {
            var module=(AppModule?) provider.GetService(type);
            module?.ConfigureServices(builder.Services);
            
        }

        return builder;

        void LookupModules(IEnumerable<Type> moduleTypes)
        {
            moduleTypes.ForEach(moduleType =>
                {
                    builder.Services.AddSingleton(moduleType);

                    //if ( module is null )
                    //{
                    //    return;
                    //}

                    var types = moduleType.GetCustomAttributes<BaseOnAttribute>().Select(m => m.ModuleType);
                    if ( types.Any() )
                    {
                        LookupModules(types);
                    }

                    //module?.ConfigureServices(builder.Services);
                    //module?.ConfigureBizer(builder);
                });
        }
    }
}