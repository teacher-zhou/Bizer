namespace Bizer.AspNetCore.Components;

/// <summary>
/// 徽章，用于表示状态。
/// </summary>
[CssClass("badge")]
[HtmlTag("span")]
public class Badge:BizerChildConentComponentBase
{
    /// <summary>
    /// 徽章颜色。
    /// </summary>
    [Parameter][CssClass("text-bg-")]public Color? Color { get; set; }

    /// <summary>
    /// 椭圆样式。
    /// </summary>
    [Parameter][CssClass("rounded-pill")]public bool Pill { get; set; }
}

