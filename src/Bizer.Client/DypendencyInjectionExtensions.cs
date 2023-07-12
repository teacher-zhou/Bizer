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
    /// <param name="services"></param>
    /// <param name="assembly">要指定的程序集。</param>
    /// <param name="configure">动态 HTTP 代理的配置。</param>
    public static IServiceCollection AddBizerClient(this IServiceCollection services, Action<DynamicHttpProxyConfiguration> configure)
    {
        var options = new DynamicHttpProxyConfiguration();
        configure.Invoke(options);

        var assemblies=options.GetMatchesAssemblies();

        var serviceTypes = assemblies.SelectMany(m=>m.GetExportedTypes()).Where(IsSubscribeToHttpProxy);

        foreach (var type in serviceTypes)
        {
            services.AddBizerClient(type, configure);
        }

        return services;

        static bool IsSubscribeToHttpProxy(Type type)
            => type.IsInterface
            && type.IsPublic
            && !type.IsGenericType
            && type.IsDefined(typeof(ApiRouteAttribute));
    }
    /// <summary>
    /// 添加指定契约服务的动态 HTTP 代理。
    /// </summary>
    /// <typeparam name="TContractService">契约服务类型。</typeparam>
    /// <param name="services"></param>
    /// <param name="configure">动态 HTTP 代理的配置。</param>
    /// <returns></returns>
    public static IServiceCollection AddBizerClient<TContractService>(this IServiceCollection services, Action<DynamicHttpProxyConfiguration> configure) where TContractService : class
    {
        return AddBizerClient(services, typeof(TContractService), configure);
    }
    /// <summary>
    /// 添加指定类型的动态 HTTP 代理。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="contractServiceType">契约服务类型。</param>
    /// <param name="configure">动态 HTTP 代理的配置。</param>
    /// <returns></returns>
    public static IServiceCollection AddBizerClient(this IServiceCollection services, Type contractServiceType, Action<DynamicHttpProxyConfiguration> configure)
    {
        Type interceptorType = AddCommonConfiguration(services, contractServiceType, configure);

        services.AddTransient(contractServiceType, provider =>
        {
            return Generator.CreateInterfaceProxyWithoutTarget(contractServiceType, ((IAsyncInterceptor)provider.GetRequiredService(interceptorType)).ToInterceptor());
        });
        return services;
    }

    private static Type AddCommonConfiguration(IServiceCollection services, Type type, Action<DynamicHttpProxyConfiguration> configure)
    {
        services.AddRemotingConverter();

        var configuration = new DynamicHttpProxyConfiguration
        {
            Name = DynamicHttpProxyConfiguration.Default
        };

        configure(configuration);

        services.Configure<DynamicHttpProxyOptions>(options =>
        {
            options.HttpProxies[type] = configuration;
        });

        var httpClientBuilder = services.AddHttpClient(configuration.Name, client => client.BaseAddress = new(configuration.BaseAddress));

        if (configuration.PrimaryHandler is not null)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(configuration.PrimaryHandler);
        }

        foreach (var handler in configuration.DelegatingHandlers)
        {
            httpClientBuilder.AddHttpMessageHandler(handler);
        }

        services.AddTransient(typeof(DynamicHttpClientProxy<>).MakeGenericType(type));

        var interceptorType = typeof(DynamicHttpInterceptor<>).MakeGenericType(type);
        services.AddTransient(interceptorType);

        return interceptorType;
    }
}
