using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public static class ToastServiceExtensions
{
    public static Task Show(this IToastService service, string? message, string? title = default, Color? color = default, int? delay = 5000, Placement placement = Placement.TopRight)
    {
        var parameters = new DynamicParameters();
        parameters.SetTitle(b => b.Content(title));
        parameters.SetContent(b => b.Content(message));
        parameters.Set("Color", color);

        return service.Show<SimpleToast>(new()
        {
            Delay = delay,
            Placement = placement,
        }, parameters);
    }

    public static Task ShowInfo(this IToastService service, string? message, string? title = "提示", int? delay = 5000, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Primary, delay, placement);

    public static Task ShowSuccess(this IToastService service, string? message, string? title = "成功", int? delay = 5000, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Success, delay, placement);

    public static Task ShowWarning(this IToastService service, string? message, string? title = "警告", int? delay = 5000, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Warning, delay, placement);

    public static Task ShowDanger(this IToastService service, string? message, string? title = "错误", int? delay = 5000, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Danger, delay, placement);
}
