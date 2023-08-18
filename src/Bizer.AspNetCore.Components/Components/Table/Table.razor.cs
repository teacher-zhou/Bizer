using Bizer.Services;

using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Bizer.AspNetCore.Components;

[CssClass("table")]
public partial class Table<TItem>
{
    /// <summary>
    /// 鼠标悬停的行背景效果。
    /// </summary>
    [Parameter][CssClass("table-hover")]public bool Hover { get; set; }

    /// <summary>
    /// 行间隔颜色。
    /// </summary>
    [Parameter][CssClass("table-striped")]public bool StripedRow { get; set; }
    /// <summary>
    /// 列间隔色。
    /// </summary>
    [Parameter][CssClass("table-striped-columns")] public bool StripedColumn { get; set; }
    /// <summary>
    /// 单元格边框。<c>true</c> 有边框，<c>false</c> 无边框，<c>null</c> 默认。
    /// </summary>
    [Parameter][BooleanCssClass("table-bordered","table-borderless")]public bool? Bordered { get; set; }
    /// <summary>
    /// 压缩内边距。
    /// </summary>
    [Parameter][CssClass("table-sm")]public bool Small { get; set; }
    /// <summary>
    /// 固定高度。
    /// </summary>
    [Parameter] public string? Height { get; set; } = "80vh";
    /// <summary>
    /// 数据源。
    /// </summary>
    [Parameter][EditorRequired] public IEnumerable<TItem> DataSource { get; set; } = Enumerable.Empty<TItem>();

    /// <summary>
    /// 头部模板。
    /// </summary>
    [Parameter]public RenderFragment? HeaderTemplate { get; set; }
    /// <summary>
    /// 行模板。
    /// </summary>
    [Parameter][EditorRequired]public RenderFragment<TItem>? RowTemplate { get; set; }
    /// <summary>
    /// 底部模板。
    /// </summary>
    [Parameter] public RenderFragment? FooterTemplate { get; set; }
    /// <summary>
    /// 表格的标题。
    /// </summary>
    [Parameter]public string? Caption { get; set; }
    /// <summary>
    /// 表格的标题在上方呈现。
    /// </summary>
    [Parameter][CssClass("caption-top")]public bool CaptionOnTop { get; set; }
    /// <summary>
    /// thead 的样式。
    /// </summary>
    [Parameter] public string? HeaderClass { get; set; }
    /// <summary>
    /// tbody 的样式。
    /// </summary>
    [Parameter] public string? BodyClass { get; set; }
    /// <summary>
    /// tfoot 的样式。
    /// </summary>
    [Parameter] public string? FooterClass { get; set; }

    /// <summary>
    /// 行可以被选中。
    /// </summary>
    [Parameter]public bool RowSelection { get; set; }
    /// <summary>
    /// 行选中后的背景颜色。
    /// </summary>
    [Parameter] public Color? RowSelectedColor { get; set; }

    /// <summary>
    /// 当行被选中后触发的回调方法。
    /// </summary>
    [Parameter]public EventCallback<TItem> OnRowSelected { get; set; }

    TItem? SelectedItem { get; set; }

    string? RowSelectedClass { get; set; }


    List<TItem> CollectionList => new(DataSource);

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if(DataSource is null)
        {
            throw new ArgumentNullException(nameof(DataSource));
        }
    }

    async Task SelectRow(TItem item)
    {
        if(item is null)
        {
            return;
        }

        SelectedItem = item;
        await OnRowSelected.InvokeAsync(SelectedItem);
        RowSelectedClass = HtmlHelper.Instance.Class().Append("table-active").Append(RowSelectedColor?.GetCssClass("bg-")).ToString();
        await this.Refresh();
    }

    RenderFragment BuildRow(TItem item)
        => builder => builder.Element("tr", RowSelectedClass)
                            .Class("cursor-pointer", RowSelection)
                            .Callback<MouseEventArgs>("onclick", this, e => SelectRow(item), RowSelection)
                            .Content(RowTemplate, item)
                            .Close();
}
