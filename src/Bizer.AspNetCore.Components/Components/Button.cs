namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示具备点击功能的 button 元素。
/// </summary>
[HtmlTag("button")]
[CssClass("btn")]
[ChildComponent(typeof(ButtonGroup),Optional =true)]
public class Button : BizerChildConentComponentBase
{
    public Button()
    {
        PreventDropDownToggleClass = true;
    }

    [CascadingParameter]public ButtonGroup? CascadingButtonGroup { get; set; }
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

    /// <summary>
    /// 当在 <see cref="ButtonGroup"/> 支持 <see cref="DropDown"/> 组件时设为 <c>true</c>。
    /// </summary>
    [Parameter]public bool DropDownInButtonGroup { get; set; }
    /// <summary>
    /// <see cref="DropDown"/> 组件中有效，自动生成下拉菜单的图标，ChildContent 的内容会忽略。
    /// </summary>
    [Parameter][CssClass("dropdown-toogle-split")]public bool DropDownSplit { get; set; }

    protected override bool CanDropDown 
        => (CascadingDropDown is not null && CascadingButtonGroup is not null && DropDownInButtonGroup)
        || CascadingDropDown is not null && CascadingButtonGroup is null;

    protected override void AfterSetParameters(ParameterView parameters)
    {
        base.AfterSetParameters(parameters);
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        if (DropDownSplit && CascadingDropDown is not null)
        {
            builder.Span("visually-hidden").Content("DropDown").Close();
        }
        else
        {
            base.AddContent(builder, sequence);
        }
    }

    /// <inheritdoc/>
    protected override void BuildCssClass(ICssClassBuilder builder)
    {

        if (Color.HasValue)
        {
            builder.Append(Color.GetCssClass("btn-"), !Outline);
            builder.Append(Color.GetCssClass("btn-outline-"), Outline);
        }

        base.BuildCssClass(builder);

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