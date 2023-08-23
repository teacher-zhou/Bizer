using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public static class ToastServiceExtensions
{
    /// <summary>
    /// 从右下方出现的通知。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">内容。</param>
    /// <param name="title">标题。</param>
    /// <param name="color">颜色。</param>
    /// <returns></returns>
    public static Task ShowNotification(this IToastService service, string? message, string? title = default, Color? color = default)
    {
        var parameters = new DynamicParameters();
        parameters.SetTitle(b => b.Content(title));
        parameters.SetContent(b => b.Content(message));
        parameters.Set("Color", color);

        return service.Show<SimpleToast>(new()
        {
            Placement = Placement.BottomRight,
        }, parameters);
    }

    /// <summary>
    /// 从右下方出现的提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="title">标题。</param>
    /// <returns></returns>
    public static Task ShowNotificationInfo(this IToastService service, string? message, string? title = "提示")
        => service.ShowNotification(message, title, Color.Primary);

    /// <summary>
    /// 从右下方出现的成功。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="title">标题。</param>
    public static Task ShowNotificationSuccess(this IToastService service, string? message, string? title = "成功")
        => service.ShowNotification(message, title, Color.Success);

    /// <summary>
    /// 从右下方出现的警告。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="title">标题。</param>
    public static Task ShowNotificationWarning(this IToastService service, string? message, string? title = "警告")
        => service.ShowNotification(message, title, Color.Warning);

    /// <summary>
    /// 从右下方出现的错误。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="title">标题。</param>
    public static Task ShowNotificationError(this IToastService service, string? message, string? title = "错误")
        => service.ShowNotification(message, title, Color.Danger);

    /// <summary>
    /// 从右上方出现的背景提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="color">颜色</param>
    /// <returns></returns>
    public static Task ShowTip(this IToastService service, string? message, Color? color = default)
    {
        var parameters = new DynamicParameters();
        parameters.SetContent(b => b.Content(message));
        parameters.Set("Color", color);
        parameters.Set("ColorFilled", true);

        return service.Show<ColoredToast>(new()
        {
            Placement =  Placement.TopRight,
        }, parameters);
    }


    /// <summary>
    /// 从右上方出现的信息提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowTipInfo(this IToastService service, string? message)
        => service.ShowTip(message, color: Color.Primary);

    /// <summary>
    /// 从右上方出现的成功提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowTipSuccess(this IToastService service, string? message)
        => service.ShowTip(message, color: Color.Success);

    /// <summary>
    /// 从右上方出现的警告提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowTipWarning(this IToastService service, string? message)
        => service.ShowTip(message, color: Color.Warning);

    /// <summary>
    /// 从右上方出现的错误提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowTipError(this IToastService service, string? message)
        => service.ShowTip(message, color:Color.Danger);

    /// <summary>
    /// 从顶部上方出现的操作提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    /// <param name="color">颜色。</param>
    /// <returns></returns>
    public static Task ShowOperation(this IToastService service, string? message, Color? color = default)
    {
        var parameters = new DynamicParameters();
        
        parameters.SetContent(b => b.Content(message));
        parameters.Set("Color", color);

        return service.Show<OperationToast>(new()
        {
            Placement =  Placement.TopCenter,
        }, parameters);
    }

    /// <summary>
    /// 从顶部上方出现的操作信息提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>

    public static Task ShowOperationInfo(this IToastService service, string? message)
        => service.ShowOperation(message, color: Color.Primary);

    /// <summary>
    /// 从顶部上方出现的操作成功提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowOperationSuccess(this IToastService service, string? message)
        => service.ShowOperation(message, color: Color.Success);

    /// <summary>
    /// 从顶部上方出现的操作警告提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowOperationWarning(this IToastService service, string? message)
        => service.ShowOperation(message, color: Color.Warning);

    /// <summary>
    /// 从顶部上方出现的操作错误提示。
    /// </summary>
    /// <param name="service"></param>
    /// <param name="message">消息。</param>
    public static Task ShowOperationError(this IToastService service, string? message)
        => service.ShowOperation(message, color: Color.Danger);
}
