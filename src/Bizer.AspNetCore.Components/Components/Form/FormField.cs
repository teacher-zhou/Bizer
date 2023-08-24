using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;


/// <summary>
/// 自动识别表单字段、布局等操作。
/// </summary>
/// <typeparam name="TValue"></typeparam>
[ChildComponent(typeof(Form))]
public class FormField<TValue> : BizerChildConentComponentBase
{

    [CascadingParameter] public Form CascadingForm { get; set; }
    [CascadingParameter] public EditContext? EditContext { get; private set; }
    /// <summary>
    /// 用于识别的字段。
    /// </summary>
    [Parameter][EditorRequired] public Expression<Func<TValue>> For { get; set; }
    /// <summary>
    /// 显示文本。 <c>null</c> 则自动识别 <see cref="DisplayAttribute.Name"/> 或 <see cref="DisplayNameAttribute.DisplayName"/> 作为文本。
    /// </summary>
    [Parameter]public string? Label { get; set; }
    /// <summary>
    /// 尺寸占比。
    /// </summary>
    [Parameter] public (Columns label, Columns control,Columns validation) Size { get; set; } = new(Columns.Is12, Columns.Is12,Columns.Is12);
    /// <summary>
    /// 自适应行内控件。
    /// </summary>
    [Parameter]public bool AutoInline { get; set; }

    /// <summary>
    /// 行间隔。
    /// </summary>
    [Parameter][CssClass("mb-")] public Space? Space { get; set; } = Components.Space.Is3;

    FieldIdentifier Field => FieldIdentifier.Create(For);
    /// <summary>
    /// 识别 <see cref="RequiredAttribute"/> 作为必填。
    /// </summary>
    bool Required
    {
        get
        {
            var attr = GetMember().GetCustomAttribute<RequiredAttribute>();
            return attr is not null || (attr is not null && !attr.AllowEmptyStrings);
        }
    }

    /// <summary>
    /// 显示文本。
    /// </summary>
    string? DisplayLabel => Label ?? GetMember().GetCustomAttribute<DisplayAttribute>()?.Name ?? GetMember().GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? Field.FieldName;


    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (CascadingForm.AutoInline)
        {
            builder.Div("col-12")
                .Content(content =>
                {
                    AddContent(content, 10);
                    BuildValidationMessage(content);
                }).Close();
        }
        else if (AutoInline)
        {
            builder.Div("col-auto")
                .Content(content =>
                {
                    AddContent(content, 10);
                    BuildValidationMessage(content);
                }).Close();
        }
        else
        {
            builder.Div("row")
                .Class("mb-3")
                .Content(content =>
                {
                    BuildLabel(content);
                    AddContent(content, 10);
                    BuildValidationMessage(content);
                }).Close();
        }
    }


    private void BuildValidationMessage(RenderTreeBuilder builder)
    {
        builder.Component<ValidationMessage<TValue>>().Attribute(m => m.For, For).Close();
    }

    private void BuildLabel(RenderTreeBuilder builder)
    {
        builder.Component<FormLabel<TValue>>()
            .Attribute(m => m.For, For)
            .Content(DisplayLabel)
            .Close();

        builder.Span("text-danger", Required).Content("*").Close();
    }


    MemberInfo GetMember()
    {
        if (For.Body is not MemberExpression member)
        {
            throw new InvalidCastException("未能识别的 For 表达式，必须是 () => f.Field 实体表达式");
        }

        return member.Member;
    }

    
}
