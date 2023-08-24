using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表单显示的文本。
/// </summary>
[HtmlTag("label")]
[CssClass("col-form-label")]
[ChildComponent(typeof(Form))]
public class FormLabel<TValue> : BizerChildConentComponentBase
{
    /// <summary>
    /// 要识别的字段。
    /// </summary>
    [Parameter] public Expression<Func<TValue>>? For { get; set; }

    /// <summary>
    /// 尺寸。
    /// </summary>
    [Parameter][CssClass("col-form-label-")]public Size? Size { get; set; }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        if (For is not null)
        {
            attributes["for"] = FieldIdentifier.Create(For).FieldName;
        }
    }
}
