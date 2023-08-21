namespace Bizer.AspNetCore.Components;

public interface IToastService:IDisposable
{
    /// <summary>
    /// 显示指定消息提示。
    /// </summary>
    /// <param name="configuration">消息服务的配置。</param>
    Task Show(ToastConfiguration configuration);
    /// <summary>
    /// 当消息被关闭后时触发的事件。
    /// </summary>
    event Action? OnClosed;
    /// <summary>
    /// 当消息正在显示时触发的事件。
    /// </summary>

    event Func<ToastConfiguration, Task>? OnShowing;
}

internal class ToastService : IToastService
{
    public event Action? OnClosed;
    public event Func<ToastConfiguration, Task>? OnShowing;

    /// <inheritdoc/>
    public void Dispose() => OnClosed?.Invoke();

    public async Task Show(ToastConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (OnShowing is not null)
        {
            await OnShowing.Invoke(configuration);
        }
    }
}
