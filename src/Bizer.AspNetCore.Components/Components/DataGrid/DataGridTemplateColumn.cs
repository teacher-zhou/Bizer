namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示单元格的自定义模板列。
/// </summary>
/// <typeparam name="TItem">数据的类型。</typeparam>
public class DataGridTemplateColumn<TItem> : DataGridColumnBase<TItem>
{
    /// <summary>
    /// 模板任意内容。
    /// </summary>
    [Parameter]public RenderFragment<TItem>? ChildContent { get; set; }

    protected internal override RenderFragment? GetCellContent(int rowIndex, TItem item)
    => ChildContent?.Invoke(item);
}
