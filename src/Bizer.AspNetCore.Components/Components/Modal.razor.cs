namespace Bizer.AspNetCore.Components;
/// <summary>
/// 模态框。
/// </summary>
[CssClass("modal")]
partial class Modal
{
    /// <summary>
    /// 头部模板。
    /// </summary>
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }
    /// <summary>
    /// 底部模板。
    /// </summary>
    [Parameter]public RenderFragment? FooterTemplate { get; set;}

    /// <summary>
    /// 显示关闭按钮。
    /// </summary>
    [Parameter]public bool Closable { get; set; }
    /// <summary>
    /// 无法点击背景关闭对话框。
    /// </summary>
    [Parameter]public bool Static { get; set; }
    /// <summary>
    /// 超长内容允许滚动查看。
    /// </summary>
    [Parameter]public bool Scrollable { get; set; }

    /// <summary>
    /// 居中。
    /// </summary>
    [Parameter]public bool Centered { get; set; }
    /// <summary>
    /// 尺寸。
    /// </summary>
    [Parameter]public ModalSize? Size { get; set; }

    /// <summary>
    /// 是否全屏。
    /// </summary>
    [Parameter]public bool FullScreen { get; set; }

    /// <summary>
    /// 全屏应用的适配点。
    /// </summary>
    [Parameter] public IEnumerable<BreakPoint> FullScreenBreakPoints { get; set; } = Enumerable.Empty<BreakPoint>();

    string? GetDialogClass()
    {
        var builder = HtmlHelper.Instance.Class()
            .Append("modal-dialog")
            .Append("modal-dialog-scrollable", Scrollable)
            .Append("modal-dialog-centered", Centered)
            .Append(Size?.GetCssClass(), Size.HasValue)
            .Append("modal-fullscreen", !FullScreenBreakPoints.Any() && FullScreen);

        if (FullScreenBreakPoints.Any() && FullScreen)
        {
            builder.Append(FullScreenBreakPoints.Select(point => $"fullscreen-{point.GetCssClass()}-down").Aggregate((prev, next) => $"{prev} {next}"));
        }



        return builder.ToString();
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        if (Static)
        {
            attributes["data-bs-backdrop"] = "static";
        }
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("fade");
    }
}
