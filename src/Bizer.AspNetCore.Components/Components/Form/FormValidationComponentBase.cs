using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

[ChildComponent(typeof(Form))]
public abstract class FormValidationComponentBase<TValue>:BizerComponentBase,IHasInputValue<TValue>
{
    [CascadingParameter]public Form CascadingForm { get; set; }
    [CascadingParameter]public EditContext? CascadedEditContext { get; private set; }
    [Parameter]public Expression<Func<TValue?>>? ValueExpression { get; set; }
    [Parameter]public TValue? Value { get; set; }
    [Parameter]public EventCallback<TValue?> ValueChanged { get; set; }

    protected FieldIdentifier Field => FieldIdentifier.Create(ValueExpression!);

    protected void BuildFeedback(RenderTreeBuilder builder, string? content) => builder.Div("valid-feedback").Content(content).Close();
}
