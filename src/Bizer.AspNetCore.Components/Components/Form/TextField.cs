using System.Linq.Expressions;

using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 单行文本框。
/// </summary>
/// <typeparam name="TValue">值的类型。</typeparam>
[HtmlTag("input")]
[CssClass("form-control")]
public class TextField<TValue> : BizerInputBase<TValue>
{
    /// <summary>
    /// 尺寸。
    /// </summary>
    [Parameter][CssClass("form-control-")] public Size? Size { get; set; }
}
