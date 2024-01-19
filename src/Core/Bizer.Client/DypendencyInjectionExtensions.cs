using Bizer;
using Bizer.Client;

using Castle.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Bizer;

/// <summary>
/// 依赖注入的扩展。
/// </summary>
public static class DypendencyInjectionExtensions
{
    static readonly ProxyGenerator Generator = new();

    /// <summary>
    /// 添加动态 HTTP 客户端代理的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="baseAddress">访问 URI 的基本地址。</param>
    public static BizerBuilder AddDynamicHttpProxy(this BizerBuilder builder, string baseAddress)
    => builder.AddDynamicHttpProxy(configure => configure.BaseAddress = new(baseAddress));

    /// <summary>
    /// 为指定的服务添加动态 HTTP 客户端代理的服务。
    /// </summary>
    /// <typeparam name="TService">服务接口的类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="baseAddress">访问 URI 的基本地址。</param>
    public static BizerBuilder AddDynamicHttpProxy<TService>(this BizerBuilder builder, string baseAddress) where TService : class
        => builder.AddDynamicHttpProxy(configure =>
        {
            configure.BaseAddress = new(baseAddress);
            configure.Name = typeof(TService).Name;
        });

    /// <summary>
    /// 添加动态 HTTP 客户端代理的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure">一个用于配置 HTTP 客户端的委托。</param>
    public static BizerBuilder AddDynamicHttpProxy(this BizerBuilder builder, Action<HttpClientConfiguration> configure)
    {
        var options = new HttpClientConfiguration();
        configure.Invoke(options);

        var assemblies = builder.AutoDiscovery.GetDiscoveredAssemblies();

        var serviceTypes = assemblies.SelectMany(m => m.GetExportedTypes()).Where(IsSubscribeToHttpProxy);

        foreach (var type in serviceTypes)
        {
            builder.AddDynamicHttpProxy(type, configure);
        }

        return builder;

        static bool IsSubscribeToHttpProxy(Type type)
            => type.IsInterface
            && type.IsPublic
            && !type.IsGenericType
            && type.IsDefined(typeof(ApiRouteAttribute));
    }
    /// <summary>
    /// 为指定的而类型添加 HTTP 客户端的转换。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceType">标记了 <see cref="ApiRouteAttribute"/> 特性的接口类型。</param>
    /// <param name="configure">一个用于配置 HTTP 客户端的委托。</param>
    /// <returns></returns>
    static BizerBuilder AddDynamicHttpProxy(this BizerBuilder builder, Type serviceType, Action<HttpClientConfiguration> configure)
    {
        Type interceptorType = AddCommonConfiguration(builder, serviceType, configure);

        builder.Services.AddTransient(serviceType, provider =>
        {
            return Generator.CreateInterfaceProxyWithoutTarget(serviceType, ((IAsyncInterceptor)provider.GetRequiredService(interceptorType)).ToInterceptor());
        });
        return builder;
    }

    static Type AddCommonConfiguration(BizerBuilder builder, Type type, Action<HttpClientConfiguration> configure)
    {
        builder.Services.AddSingleton<IHttpRemotingResolver, DefaultHttpRemotingResolver>();

        var configuration = new HttpClientConfiguration
        {
            Name = HttpClientConfiguration.Default
        };

        configure(configuration);

        builder.Services.Configure<HttpClientOptions>(options =>
        {
            options.HttpConfigurations[type] = configuration;
        });

        var httpClientBuilder = builder.Services.AddHttpClient(configuration.Name, client => client.BaseAddress = configuration.BaseAddress);

        if (configuration.PrimaryHandler is not null)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(configuration.PrimaryHandler);
        }

        foreach (var handler in configuration.DelegatingHandlers)
        {
            httpClientBuilder.AddHttpMessageHandler(handler);
        }

        var interceptorType = typeof(HttpClientInterceptor<>).MakeGenericType(type);
        builder.Services.AddTransient(interceptorType);

        return interceptorType;
    }
}
