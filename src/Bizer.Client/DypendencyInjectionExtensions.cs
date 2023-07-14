using Bizer;
using Bizer.Client;
using Castle.DynamicProxy;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 依赖注入的扩展。
/// </summary>
public static class DypendencyInjectionExtensions
{
    static readonly ProxyGenerator Generator = new();

    /// <summary>
    /// 添加指定程序集的动态 HTTP 代理。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assembly">要指定的程序集。</param>
    /// <param name="configure">动态 HTTP 代理的配置。</param>
    public static BizerBuilder AddBizerClient(this BizerBuilder builder, Action<DynamicHttpProxyConfiguration> configure)
    {
        var options = new DynamicHttpProxyConfiguration();
        configure.Invoke(options);

        var assemblies = builder.AutoDiscovery.GetDiscoveredAssemblies();

        var serviceTypes = assemblies.SelectMany(m => m.GetExportedTypes()).Where(IsSubscribeToHttpProxy);

        foreach ( var type in serviceTypes )
        {
            builder.AddBizerClient(type, configure);
        }

        return builder;

        static bool IsSubscribeToHttpProxy(Type type)
            => type.IsInterface
            && type.IsPublic
            && !type.IsGenericType
            && type.IsDefined(typeof(ApiRouteAttribute));
    }
    /// <summary>
    /// 添加指定类型的动态 HTTP 代理。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="contractServiceType">契约服务类型。</param>
    /// <param name="configure">动态 HTTP 代理的配置。</param>
    /// <returns></returns>
    static BizerBuilder AddBizerClient(this BizerBuilder builder, Type contractServiceType, Action<DynamicHttpProxyConfiguration> configure)
    {
        Type interceptorType = AddCommonConfiguration(builder, contractServiceType, configure);

        builder.Services.AddTransient(contractServiceType, provider =>
        {
            return Generator.CreateInterfaceProxyWithoutTarget(contractServiceType, ((IAsyncInterceptor)provider.GetRequiredService(interceptorType)).ToInterceptor());
        });
        return builder;
    }

    private static Type AddCommonConfiguration(BizerBuilder builder, Type type, Action<DynamicHttpProxyConfiguration> configure)
    {
        builder.AddHttpRemotingResolver();

        var configuration = new DynamicHttpProxyConfiguration
        {
            Name = DynamicHttpProxyConfiguration.Default
        };

        configure(configuration);

        builder.Services.Configure<DynamicHttpProxyOptions>(options =>
        {
            options.HttpProxies[type] = configuration;
        });

        var httpClientBuilder = builder.Services.AddHttpClient(configuration.Name, client => client.BaseAddress = new(configuration.BaseAddress));

        if (configuration.PrimaryHandler is not null)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(configuration.PrimaryHandler);
        }

        foreach (var handler in configuration.DelegatingHandlers)
        {
            httpClientBuilder.AddHttpMessageHandler(handler);
        }

        builder.Services.AddTransient(typeof(DynamicHttpClientProxy<>).MakeGenericType(type));

        var interceptorType = typeof(DynamicHttpInterceptor<>).MakeGenericType(type);
        builder.Services.AddTransient(interceptorType);

        return interceptorType;
    }
}
