namespace Bizer.AspNetCore.Components;

/// <summary>
/// 多行文本框。
/// </summary>
public class TextArea:BizerInputBase<string?>
{
    /// <summary>
    /// 文本框高度的行数。
    /// </summary>
    [Parameter][HtmlAttribute] public int? Rows { get; set; } = 3;
}
