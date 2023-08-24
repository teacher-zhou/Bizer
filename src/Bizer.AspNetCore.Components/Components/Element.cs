namespace Bizer.AspNetCore.Components;

public class Element:BizerChildConentComponentBase
{
    [Parameter] public string Tag { get; set; } = "div";

    public override string GetTagName() => Tag;
}

public class Div : BizerChildConentComponentBase { }
[HtmlTag("span")]
public class Span : BizerChildConentComponentBase
{

}