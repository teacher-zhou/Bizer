namespace Bizer.AspNetCore.Components;

/// <summary>
/// 指示器，永远显示在右上角的小红圈。
/// </summary>
public class Indicator : BizerComponentBase
{
    /// <summary>
    /// 指示器的文本。没有文本则显示小红点。
    /// </summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>
    /// 当 <see cref="Text"/> 空时有效，使用更小的圆点。
    /// </summary>
    [Parameter] public bool Dot { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Span("position-absolute")
                .Class("top-0 start-100 translate-middle")
                .Class("bg-danger")
                .Class($"rounded-circle border border-light p-{(Dot ? "1" : "2")}", string.IsNullOrEmpty(Text))
                .Class("badge rounded-pill", !string.IsNullOrEmpty(Text))
                .Content(content => content.Content(Text).Span("visually-hidden").Content(Text).Close())
             .Close();
    }
}
