using Bizer.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Bizer.Services;


/// <summary>
/// 表示基本的业务服务的基类。
/// <para>
/// 这是一个空的基类，只提供了一些基本的服务，例如 <see cref="ILogger"/> 对象。
/// </para>
/// </summary>
public abstract class ServiceBase
{
    /// <summary>
    /// 初始化 <see cref="ServiceBase"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    protected ServiceBase(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

    /// <summary>
    /// 获取添加的服务的提供器。
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取日志对象。
    /// </summary>
    /// <value>如果没有添加 <c>services.AddLogging();</c> 服务，则可能返回 <c>null</c>。
    /// <para>
    /// 推荐使用可空表达式（?.）来调用：<c>Logger?.LogInfo(message);</c>
    /// </para>
    /// </value>
    protected ILogger? Logger => ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(this.GetType().Name);

    /// <summary>
    /// 获取当前主体。
    /// </summary>
    protected ICurrentPrincipalAccessor? CurrentPricipal => ServiceProvider.GetService<ICurrentPrincipalAccessor>();

    /// <summary>
    /// 获取本地化器。
    /// </summary>
    protected IStringLocalizer? Locale => (IStringLocalizer?)ServiceProvider.GetService(typeof(IStringLocalizer<>));
}
