using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public interface IToastService:IDisposable
{
    /// <summary>
    /// 显示指定消息提示。
    /// </summary>
    /// <param name="configuration">消息服务的配置。</param>
    Task Show<TToastTemplate>(ToastConfiguration configuration,DynamicParameters? parameters=default) where TToastTemplate : IComponent;
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

    public async Task Show<TToastTemplate>(ToastConfiguration configuration, DynamicParameters? parameters = default) where TToastTemplate : IComponent
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        parameters ??= new();
        parameters.SetDynamicTemplate<TToastTemplate>();

        configuration.Parameters = parameters;

        if (OnShowing is not null)
        {
            await OnShowing.Invoke(configuration);
        }
    }
}
