using Microsoft.Extensions.Configuration;

namespace Bizer;

/// <summary>
/// 应用程序全局对象。
/// </summary>
public class App
{
    private static IServiceProvider? _serviceProvider;
    /// <summary>
    /// 初始化应用程序。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> 是 <c>null</c>。</exception>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        if (serviceProvider is null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 使用懒加载的方式获取服务。
    /// </summary>
    public static IServiceProvider Services
    {
        get
        {
            if (_serviceProvider is null)
            {
                throw new ArgumentNullException($"请在 builder.Build() 后面添加 .WithBizer() 方法，示例：var app = builder.Build().WithBizer();");
            }
            return _serviceProvider;
        }
    }

    /// <summary>
    /// 获取 <see cref="IConfiguration"/> 的实例。
    /// </summary>
    public static IConfiguration? Configuration => _serviceProvider?.GetService<IConfiguration>();
}
