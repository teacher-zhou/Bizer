namespace Bizer.AspNetCore.Components;

/// <summary>
/// 输入组件的基类。
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class BizerInputBase<TValue> : FormValidationComponentBase<TValue>
{   
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
