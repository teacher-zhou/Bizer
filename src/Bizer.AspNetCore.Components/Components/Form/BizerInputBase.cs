using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 输入组件的基类。
/// </summary>
/// <typeparam name="TValue"></typeparam>
[ChildComponent(typeof(Form), Optional = true)]
public abstract class BizerInputBase<TValue> : BizerComponentBase, IHasInputValue<TValue>
{
    [CascadingParameter] public EditContext? CascadedEditContext { get;private set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public TValue? Value { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public Expression<Func<TValue?>>? ValueExpression { get; set; }
    /// <summary>
    /// 只读状态。
    /// </summary>
    [Parameter][HtmlAttribute] public bool ReadOnly { get; set; }
    /// <summary>
    /// 纯文本状态。
    /// </summary>
    [Parameter][CssClass("form-control-plaintext")] public bool AsText { get; set; }
    /// <summary>
    /// 提示占位字符串。
    /// </summary>
    [Parameter][HtmlAttribute] public string? PlaceHolder { get; set; }
    /// <summary>
    /// 禁用状态。
    /// </summary>
    [Parameter][HtmlAttribute] public bool Disabled { get; set; }

    /// <summary>
    /// 文本框类型。
    /// </summary>
    [Parameter][HtmlAttribute] public virtual string Type { get; set; } = "text";
    /// <summary>
    /// 输入的响应事件，默认 oninput，可以切换为 onchange。
    /// </summary>
    protected virtual string EventName => "oninput";

    protected override void AfterSetParameters(ParameterView parameters)
    {
        base.AfterSetParameters(parameters);

        this.InitializeInputValue();
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["value"] = this.GetValueAsString();
        attributes[EventName] = this.CreateValueChangedCallback();
    }

    protected override void DisposeComponentResources()
    {
        this.DisposeInputValue();
    }
}
