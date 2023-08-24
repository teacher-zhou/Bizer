namespace Bizer.AspNetCore.Components;

[ParentComponent]
[CssClass("input-group")]
public class FormInputGroup:BizerChildConentComponentBase
{
    [Parameter][CssClass("input-group-")] public Size? Size { get; set; }
}

[ChildComponent(typeof(FormInputGroup))]
[CssClass("input-group-text")]
public class InputGroupText : BizerChildConentComponentBase
{

}
