namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示表格的单元格的基类。
/// </summary>
public abstract class DataGridColumnBase<TItem>:BizerComponentBase
{
    [CascadingParameter(Name = "DataGrid")] protected internal DataGrid<TItem> CascadingDataGrid { get; set; }
    /// <summary>
    /// 设置列标题。若设置了 <see cref="HeaderContent"/> 参数，则该参数无效。
    /// </summary>
    [Parameter] public string? Header { get; set; }
    /// <summary>
    /// 设置列标题部分的任意 UI 片段。
    /// </summary>
    [Parameter] public RenderFragment? HeaderContent { get; set; }
    /// <summary>
    /// 设置标题部分的额外 CSS 类字符串。
    /// </summary>
    [Parameter] public string? HeaderClass { get; set; }

    /// <summary>
    /// 设置每一列的额外 CSS 类的字符串。
    /// </summary>
    [Parameter] public string? ColumnClass { get; set; }

    /// <summary>
    /// 列的宽度。
    /// </summary>
    [Parameter]public string? Width { get; set; }
    /// <summary>
    /// 设置底部的任意 UI 片段。
    /// </summary>
    [Parameter] public RenderFragment? FooterContent { get; set; }


    /// <inheritdoc/>
    protected override void AfterSetParameters(ParameterView parameters)
    {
        base.AfterSetParameters(parameters);

        HeaderContent ??= builder => builder.AddContent(0, Header);
    }


    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (CascadingDataGrid is null)
        {
            throw new InvalidOperationException($"列必须定义在 {nameof(DataGrid<TItem>)} 组件中");
        }

        Header ??= $"标题{CascadingDataGrid.ChildComponents.Count}";

        CascadingDataGrid.AddChildComponent(this);
        base.OnInitialized();
    }
    /// <summary>
    /// 获取标题内容。
    /// </summary>
    /// <returns></returns>
    protected internal virtual RenderFragment? GetHeaderContent() => HeaderContent;

    /// <summary>
    /// 获取单元格的内容。
    /// </summary>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="item">数据的每一项。</param>
    protected internal abstract RenderFragment? GetCellContent(int rowIndex, TItem item);

    protected override void BuildStyle(IStyleBuilder builder)
    {
        builder.Append($"width:{Width}", !string.IsNullOrEmpty(Width));
    }
}
