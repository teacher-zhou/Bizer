namespace Bizer.AspNetCore.Components;

/// <summary>
/// 图标组件。
/// </summary>
[HtmlTag("i")]
public class Icon:BizerComponentBase
{
    /// <summary>
    /// 图标名称，支持 <see cref="IconName"/> 枚举或自定义字符串。
    /// </summary>
    [Parameter][CssClass("bi-")]public object? Name { get; set; }
}
