namespace Bizer.AspNetCore.Components;

/// <summary>
/// 下拉框。
/// </summary>
/// <typeparam name="TValue">值的类型。</typeparam>
[HtmlTag("select")]
[CssClass("form-select")]
[ParentComponent]
public class FormSelect<TValue> : BizerInputBase<TValue>,IHasChildContent
{
    /// 尺寸。
    /// </summary>
    [Parameter][CssClass("form-select-")] public Size? Size { get; set; }
    /// <summary>
    /// 多选。
    /// </summary>
    [Parameter][HtmlAttribute] public bool Multiple { get; set; }
    /// <summary>
    /// 多选项。
    /// </summary>
    [Parameter][HtmlAttribute("size")] public int? MultipleSize { get; set; }
    [Parameter]public RenderFragment? ChildContent { get; set; }

    protected override string EventName => "onchange";
}

/// <summary>
/// 下拉框选项。<see cref="FormSelect{TValue}"/> 组件中使用。
/// </summary>
/// <typeparam name="TValue"></typeparam>
[HtmlTag("option")]
[CascadingTypeParameter("Value")]
public class FormSelectOption<TValue> : BizerChildConentComponentBase
{
    [CascadingParameter]public FormSelect<TValue?> CascadingSelect { get; set; }

    /// <summary>
    /// 选项的值。
    /// </summary>
    [Parameter][HtmlAttribute]public TValue? Value { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(CascadingSelect is null)
        {
            throw new InvalidOperationException($"SelectOption 必须在 Select 组件中使用");
        }
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        if (CascadingSelect.Value.Equals(Value))
        {
            attributes["selected"] = true;
        }
    }
}
