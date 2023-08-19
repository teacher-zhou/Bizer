namespace Bizer.AspNetCore.Components;

/// <summary>
/// 按钮组。
/// </summary>
[ParentComponent]
public class ButtonGroup:BizerChildConentComponentBase
{
    /// <summary>
    /// 工具栏样式。
    /// </summary>
    [Parameter]public bool Toolbar { get; set; }

    /// <summary>
    /// 尺寸。
    /// </summary>
    [Parameter][CssClass("btn-group-")]public Size? Size { get; set; }

    /// <summary>
    /// 垂直显示。
    /// </summary>
    [Parameter][CssClass("btn-group-vertical")]public bool Vertical { get; set; }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("btn-group", !Toolbar).Append("btn-toolbar", Toolbar);
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["role"] = Toolbar ? "toolbar" : "group";
    }
}
