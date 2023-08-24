namespace Bizer.AspNetCore.Components;

[CssClass("row")]
public class Row:BizerChildConentComponentBase
{
    /// <summary>
    /// 平均列数。使用 <see cref="Class.RowColumns"/> 。
    /// </summary>
    [Parameter]public IGridRowColumnSize? RowColumns { get; set; }
}

[CssClass("col")]
public class Column : BizerChildConentComponentBase
{
    /// <summary>
    /// 列占比。使用 <see cref="Class.Columns"/> 。
    /// </summary>
    [Parameter]public IGridColumnSize? Size { get; set; }
    /// <summary>
    /// 距离左边的偏移量。
    /// </summary>
    [Parameter]public IOffsetSize? Offset { get; set; }
}