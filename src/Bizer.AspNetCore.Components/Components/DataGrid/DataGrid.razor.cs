using Bizer.Services;

namespace Bizer.AspNetCore.Components;
[CssClass("table")]
[CascadingTypeParameter(nameof(TItem))]
public partial class DataGrid<TItem>
{
    /// <summary>
    /// 鼠标悬停的行背景效果。
    /// </summary>
    [Parameter][CssClass("table-hover")] public bool Hoverable { get; set; }

    /// <summary>
    /// 行间隔颜色。
    /// </summary>
    [Parameter][CssClass("table-striped")] public bool StripedRow { get; set; }
    /// <summary>
    /// 列间隔色。
    /// </summary>
    [Parameter][CssClass("table-striped-columns")] public bool StripedColumn { get; set; }
    /// <summary>
    /// 单元格边框。<c>true</c> 有边框，<c>false</c> 无边框，<c>null</c> 默认。
    /// </summary>
    [Parameter][BooleanCssClass("table-bordered", "table-borderless")] public bool? Bordered { get; set; }
    /// <summary>
    /// 压缩内边距。
    /// </summary>
    [Parameter][CssClass("table-sm")] public bool Small { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public RenderFragment? Columns { get; set; }

    [Parameter] public int PageNumber { get; set; } = 7;

    [Parameter] public int PageSize { get; set; } = 10;

    [Parameter] public EventCallback<int> OnPageChanged { get; set; }

    [Parameter][EditorRequired] public Func<PagedInput, Task<PagedOutput<TItem>>> DataSourceHandler { get; set; }

    #region Internal

    IEnumerable<TItem> DataSource { get; set; } = Enumerable.Empty<TItem>();

    IEnumerable<DataGridColumnBase<TItem>> GetColumns() => base.ChildComponents.OfType<DataGridColumnBase<TItem>>();

    bool Loading { get; set; }


    int PageIndex { get; set; } = 1;

    long Total { get; set; }

    /// <summary>
    /// 获取总页数。
    /// </summary>
    int TotalPages
    {
        get
        {
            var total = Total + PageSize - 1;
            if (total <= 0)
            {
                total = 1;
            }

            var result = total / PageSize;
            if (result < 0)
            {
                result = 1;
            }
            return (int)result;
        }
    }

    async Task LoadDataSource()
    {
        Loading = true;
        StateHasChanged();
        try
        {
            var result = await DataSourceHandler!.Invoke(new(PageIndex, PageSize));
            DataSource = result.Items;
            Total = result.Total;
        }
        catch (Exception ex)
        {
            throw ex;//先抛出
        }
        finally
        {
            Loading = false;
            StateHasChanged();
        }
    } 

    #endregion
}
