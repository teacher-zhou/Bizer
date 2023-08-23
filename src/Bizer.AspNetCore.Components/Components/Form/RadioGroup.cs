using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

[ParentComponent(IsFixed = true)]
[CascadingTypeParameter(nameof(TValue))]
public class RadioGroup<TValue> : BizerComponentBase, IHasInputValue<TValue>,IHasChildContent
{
    [CascadingParameter]public EditContext? CascadedEditContext { get; private set; }
    [Parameter]public Expression<Func<TValue?>>? ValueExpression { get; set; }
    [Parameter]public TValue? Value { get; set; }
    [Parameter]public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public EventCallback<string> OnRadioValueChanged { get; set; }
    [Parameter]public bool Disabled { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    internal EventCallback<ChangeEventArgs>? ChangeEventCallback { get; set; }

    /// <summary>
    /// Gets the selected value.
    /// </summary>
    internal TValue? SelectedValue => this.Value;

    string? _oldValue;
    /// <summary>
    /// 表示当组内的单选框发生变化时的通知。
    /// </summary>
    internal event Action NotifyRadioInputRendered;
    protected override void OnParametersSet()
    {
        var newValue = this.GetValueAsString();
        ChangeEventCallback = EventCallback.Factory.CreateBinder<string?>(this, __value =>
        {
            this.GetCurrentValueAsString(__value);
            _ = OnRadioValueChanged.InvokeAsync(__value);
        }
        , newValue);

        if (_oldValue != newValue)
        {
            _oldValue = newValue;
            NotifyRadioInputRendered?.Invoke();
        }
    }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateCascadingComponent(this, 0, ChildContent, isFixed: true);
    }
}

public class Radio<TValue>: FormCheckBase
{
    [CascadingParameter]public RadioGroup<TValue> CascadingRadioGroup { get; set; }
    [Parameter]public TValue? Value { get; set; }
    /// <summary>
    /// 禁用状态。
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    protected override string? FormCheckClass => GetCssClassString();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Value?.GetType() != typeof(TValue))
        {
            throw new InvalidOperationException($"参数 {nameof(this.Value)} 的数据类型必须与 {typeof(RadioGroup<>).FullName} 的数据类型相同");
        }

        if (CascadingRadioGroup is not null)
        {
            CascadingRadioGroup.NotifyRadioInputRendered += StateHasChanged;

            this.Disabled = CascadingRadioGroup.Disabled;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var identifier = FieldIdentifier.Create(CascadingRadioGroup.ValueExpression!);

        base.BuildFormCheck(builder, content =>
        {
            content.Element("label", "form-check-label", sequence: 100)
                .Content(input =>
                {
                    builder.Element("input", this.GetCssClassString(), sequence: 0)
                    .Attribute("name", identifier.FieldName)
                    .Attribute("type", "radio")
                    .Attribute("checked", CascadingRadioGroup!.Value?.Equals(Value))
                    .Attribute("disabled", Disabled)
                    .Attribute("value", BindConverter.FormatValue(Value))
                    .Attribute("onchange", CascadingRadioGroup.ChangeEventCallback)
                    .Close();

                    builder.Span("ms-1").Content(ChildContent).Close();
                    ;
                })
                .Close();
        });
    }
}