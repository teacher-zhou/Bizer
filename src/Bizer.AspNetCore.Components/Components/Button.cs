namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示具备点击功能的 button 元素。
/// </summary>
[HtmlTag("button")]
[CssClass("btn")]
public class Button : BizerChildConentComponentBase
{
    /// <summary>
    /// 按钮的主题颜色。
    /// </summary>
    [Parameter] public Color? Color { get; set; }
    /// <summary>
    /// 成为边框样式。
    /// </summary>
    [Parameter] public bool Outline { get; set; }
    /// <summary>
    /// 尺寸大小。
    /// </summary>
    [Parameter][CssClass] public ButtonSize? Size { get; set; }
    /// <summary>
    /// 禁用状态。
    /// </summary>
    [Parameter][HtmlAria("disabled")] public bool Disabled { get; set; }
    /// <summary>
    /// 激活状态。
    /// </summary>
    [Parameter][CssClass("active")] public bool Active { get; set; }
    /// <summary>
    /// 按钮的 HTML 类型，默认时 type="button"。
    /// </summary>
    [Parameter][HtmlAttribute("type")] public ButtonHtmlType HtmlType { get; set; } = ButtonHtmlType.Button;

    /// <inheritdoc/>
    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        if (Color.HasValue)
        {
            builder.Append(Color.GetCssClass("btn-"), !Outline);
            builder.Append(Color.GetCssClass("btn-outline-"), Outline);
        }
    }
}
/// <summary>
/// 按钮的 HTML 类型。
/// </summary>
public enum ButtonHtmlType
{
    /// <summary>
    /// type="button" 普通按钮。
    /// </summary>
    Button,
    /// <summary>
    /// type="submit" 提交按钮。
    /// </summary>
    Submit,
    /// <summary>
    /// type="reset" 表单重置按钮。
    /// </summary>
    Reset
}