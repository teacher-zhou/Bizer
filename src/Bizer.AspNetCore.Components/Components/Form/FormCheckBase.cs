namespace Bizer.AspNetCore.Components;


public abstract class FormCheckBase:BizerChildConentComponentBase
{
    /// <summary>
    /// 颠倒布局。
    /// </summary>
    [Parameter]public bool Reserve { get; set; }
    /// <summary>
    /// 横向布局。
    /// </summary>
    [Parameter]public bool Inline { get; set; }
    /// <summary>
    /// 无标签。
    /// </summary>
    [Parameter]public bool WithoutLabel { get; set; }

    protected virtual string? FormCheckClass { get; }

    protected void BuildFormCheck(RenderTreeBuilder builder,RenderFragment? content)
    {
        builder.Div(sequence:0)
              .Class("form-check", !WithoutLabel)
              .Class("form-check-reserve", Reserve)
              .Class("form-check-inline", Inline)
              .Class(FormCheckClass)
              .Content(content).Close();
    }
}
