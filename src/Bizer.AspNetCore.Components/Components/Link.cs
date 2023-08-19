namespace Bizer.AspNetCore.Components;
/// <summary>
/// 超链接。
/// </summary>
[HtmlTag("a")]
public class Link:BizerChildConentComponentBase
{
    /// <summary>
    /// 颜色。
    /// </summary>
    [Parameter][CssClass("link-")]public Color? Color { get; set; }
}
