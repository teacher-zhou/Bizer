using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

[ParentComponent(IsFixed = true)]
[CascadingTypeParameter(nameof(TValue))]
public class FormRadioGroup<TValue> : FormValidationComponentBase<TValue>,IHasChildContent
{
    /// <summary>
    /// 当单元按钮的值改变后的回调。
    /// </summary>
    [Parameter] public EventCallback<string> OnRadioValueChanged { get; set; }

    /// <summary>
    /// 是否禁用。
    /// </summary>
    [Parameter]public bool Disabled { get; set; }

    /// <summary>
    /// 单选按钮组件的内容。
    /// </summary>
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

/// <summary>
/// 单选按钮组件。必须在 <see cref="FormRadioGroup{TValue}"/> 里使用。
/// </summary>
/// <typeparam name="TValue">值的类型。</typeparam>
public class FormRadio<TValue>: FormCheckBase
{
    [CascadingParameter]public FormRadioGroup<TValue> CascadingRadioGroup { get; set; }
    /// <summary>
    /// 单选按钮的值。
    /// </summary>
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
            throw new InvalidOperationException($"参数 {nameof(this.Value)} 的数据类型必须与 {typeof(FormRadioGroup<>).FullName} 的数据类型相同");
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