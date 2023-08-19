namespace Bizer.AspNetCore.Components;
/// <summary>
/// 旋转器，用于体现加载状态。
/// </summary>
[HtmlTag("span")]
[HtmlRole("status")]
public class Spinner:BizerComponentBase
{
    /// <summary>
    /// 扩散模式。
    /// </summary>
    [Parameter]public bool Grow { get; set; }

    /// <summary>
    /// 颜色。
    /// </summary>
    [Parameter][CssClass("text-")]public Color? Color { get; set; }

    [Parameter]public Size? Size { get; set; }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        var suffix = Grow ? "grow" : "border";

        builder.Append($"spinner-{suffix}");

        if (Size.HasValue)
        {
            builder.Append($"spinner-{suffix}-{Size?.GetCssClass()}");
        }
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.Span("visually-hidden").Content("Loading...").Close();
    }
}
