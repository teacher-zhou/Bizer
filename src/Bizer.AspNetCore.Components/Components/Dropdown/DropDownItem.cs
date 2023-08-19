namespace Bizer.AspNetCore.Components;

[ChildComponent(typeof(DropDown))]
[CssClass("dropdown-item")]
public class DropDownItem:BizerChildConentComponentBase
{
    public DropDownItem()
    {
        PreventDropDownToggleClass = true;
    }

    /// <summary>
    /// HTML 元素名称。
    /// </summary>
    [Parameter] public string? TagName { get; set; }

    public override string GetTagName() => TagName ?? "div";
}

[ChildComponent(typeof(DropDown))]
[CssClass("dropdown-divider")]
public class DropDownDivider : BizerComponentBase
{
    public DropDownDivider()
    {
        PreventDropDownToggleClass = true;
    }
}