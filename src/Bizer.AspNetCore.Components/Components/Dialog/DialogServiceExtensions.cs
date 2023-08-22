using Bizer.AspNetCore.Components.Templates;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// <see cref="IDialogService"/> 的扩展。
/// </summary>
public static class DialogServiceExtensions
{
    /// <summary>
    /// 打开指定模板组件的对话框。
    /// </summary>
    /// <typeparam name="TDialogTemplate">对话框模板。</typeparam>
    /// <param name="service"></param>
    /// <param name="content">内容。</param>
    /// <param name="title">标题。</param>
    /// <param name="parameters">自定义参数。</param>
    public static Task<IDialogReference> Open<TDialogTemplate>(this IDialogService service,
                                                               RenderFragment? content,
                                                               RenderFragment? title,
                                                               DialogConfiguration? configuration,
                                                               DialogParameters? parameters = default) where TDialogTemplate : IComponent
    {
        parameters ??= new DialogParameters();

        parameters.SetTitle(title);
        parameters.SetContent(content);

        return service.Open<TDialogTemplate>(configuration,parameters);
    }
    /// <summary>
    /// 打开指定模板组件的对话框。
    /// </summary>
    /// <typeparam name="TDialogTemplate">对话框模板。</typeparam>
    /// <param name="service"></param>
    /// <param name="content">内容。</param>
    /// <param name="title">标题。</param>
    /// <param name="parameters">自定义参数。</param>
    public static Task<IDialogReference> Open<TDialogTemplate>(this IDialogService service,
                                                               string? content,
                                                               string? title,
                                                               DialogConfiguration? configuration=default,
                                                               DialogParameters? parameters = default) where TDialogTemplate : IComponent
        => service.Open<TDialogTemplate>(builder => builder.AddContent(0, content), builder => builder.AddContent(0, title), configuration, parameters);
    /// <summary>
    /// 打开显示消息的对话框。
    /// </summary>
    /// <param name="content">内容。</param>
    /// <param name="title">标题。</param>
    public static Task<IDialogReference> OpenMessage(this IDialogService dialogService, string? content = default, string? title = "提示",
                                                               DialogConfiguration? configuration = default)
    => dialogService.Open<MessageDialogTemplate>(content, title, configuration);


    /// <summary>
    /// 打开具备确认和取消操作的对话框。
    /// </summary>
    /// <param name="content">内容。</param>
    /// <param name="title">标题。</param>
    public static Task<IDialogReference> OpenConfirm(this IDialogService service,
                                                     string? content,
                                                     string? title = "确定吗", DialogConfiguration? configuration = default)
        => service.Open<ConfirmationDialogTemplate>(content, title, configuration);
}
