using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 复选框
/// </summary>
[HtmlTag("input")]
[CssClass("form-check-input")]
public class FormCheckBox : FormCheckBase, IHasInputValue<bool>
{
    [CascadingParameter]public EditContext? CascadedEditContext { get; private set; }
    [Parameter]public Expression<Func<bool>>? ValueExpression { get; set; }
    [Parameter]public bool Value { get; set; }
    [Parameter]public EventCallback<bool> ValueChanged { get; set; }

    /// <summary>
    /// 禁用状态。
    /// </summary>
    [Parameter]public bool Disabled { get; set; }

    [Parameter]public bool Switch { get; set; }

    protected override string? FormCheckClass => Switch ? "form-switch" : "";

    

    protected override void AfterSetParameters(ParameterView parameters)
    {
        base.AfterSetParameters(parameters);

        this.InitializeInputValue();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var identifier = FieldIdentifier.Create(ValueExpression!);
        base.BuildFormCheck(builder, content =>
        {
            content.Element("label", "form-check-label",sequence:100)
                .Content(input =>
                {

                    input.Element("input", this.GetCssClassString(), sequence: 0)
                    .Attribute("name", identifier.FieldName)
                    .Attribute("type", "checkbox")
                    .Attribute("checked", Value)
                    .Attribute("disabled", Disabled)
                    .Attribute("onchange", HtmlHelper.Instance.Callback().CreateBinder<bool>(this, _value =>
                    {
                        this.OnValueChanged(_value);
                        CascadedEditContext?.NotifyFieldChanged(identifier);
                    }, Value))
                    .Close();

                    builder.Span("ms-1").Content(ChildContent).Close();
                })
                .Close();
        });
    }


    protected override void DisposeComponentResources()
    {
        this.DisposeInputValue();
    }
}
