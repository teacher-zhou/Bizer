using Microsoft.Extensions.Configuration;

namespace Bizer;

/// <summary>
/// 应用程序全局上下文。
/// </summary>
public class ApplicationContext
{
    private static IServiceProvider? _serviceProvider;
    /// <summary>
    /// 设置应用程序服务。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> 是 <c>null</c>。</exception>
    public static void SetApplicationService(IServiceProvider serviceProvider)
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
}
