namespace Bizer.AspNetCore.Components;

public static class ToastServiceExtensions
{
    public static Task Show(this IToastService service, string? message, string? title = default, Color? color = default, int? delay = 5000, bool closable = true, Placement placement = Placement.TopRight)
        => service.Show(new()
        {
            Title = title,
            Body = message,
            Color = color,
            Closable = closable,
            Delay = delay,
            Placement = placement,
        });

    public static Task ShowInfo(this IToastService service, string? message, string? title = "提示", int? delay = 5000, bool closable = true, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Primary, delay, false, placement);

    public static Task ShowSuccess(this IToastService service, string? message, string? title = "成功", int? delay = 5000, bool closable = true, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Success, delay, false, placement);

    public static Task ShowWarning(this IToastService service, string? message, string? title = "警告", int? delay = 5000, bool closable = true, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Warning, delay, false, placement);

    public static Task ShowDanger(this IToastService service, string? message, string? title = "错误", int? delay = 5000, bool closable = true, Placement placement = Placement.TopRight)
        => service.Show(message, title, Color.Danger, delay, false, placement);
}
