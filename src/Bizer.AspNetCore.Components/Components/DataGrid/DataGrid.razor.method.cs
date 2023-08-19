namespace Bizer.AspNetCore.Components;
partial class DataGrid<TItem>
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            await LoadDataSource();
        }
    }


    void GetPageItem(RenderTreeBuilder builder, string? text, string? title=default, bool active=default, bool disabled = default, Func<Task>? click = default)
            => builder.Element("li", "page-item")
                                .Class("active", active)
                                .Class("disabled", disabled)
                                .Attribute("title",title)
                                .Callback("onclick",this,click,!disabled)
                                .Content(link =>
                                {
                                    link.Element(active ? "span":"a", "page-link").Attribute("href","javascript:;").Content(text).Close();
                                })
                                .Close();


    /// <summary>
    /// 构建分页。
    /// </summary>
    RenderFragment? BuildPagination(string? additionalClass=default)
        => builder => builder.Element("ul", "pagination")
                        .Class("pagination-sm",Small)
                        .Class(additionalClass,!string.IsNullOrEmpty(additionalClass))
                        .Content(content =>
                        {
                            #region 第一页

                            //第一页永远显示
                            GetPageItem(content, "首页", disabled: PageIndex == 1,click: NavigateToFirst);

                            #endregion

                            #region 前5页
                            if (PageIndex > PageNumber / 2)
                            {
                                var backTo = PageIndex - 5;
                                if (backTo <= 1)
                                {
                                    backTo = 1;
                                }
                                GetPageItem(content, "...",title:"前5页", active: PageIndex== backTo, click: () => NavigateToPage(backTo));
                            }
                            #endregion


                            #region 页码条

                            var (start, end) = ComputePageNumber();

                            //页码1 永远显示，所有从2开始
                            //最后一页永远显示，所以结束要少一个索引
                            var offset = 0;
                            for (var i = start + offset; i <= end - offset; i++)
                            {
                                var current = i;
                                var contentSequence = (int)i + 30;

                                GetPageItem(content, current.ToString(),active: PageIndex == current, click: () => NavigateToPage(current));
                            }
                            #endregion

                            #region 后5页
                            if (PageIndex < TotalPages - PageNumber / 2)
                            {
                                var nextTo = PageIndex + 5;
                                if (nextTo >= TotalPages)
                                {
                                    nextTo = TotalPages;
                                }
                                GetPageItem(content, "...", title: "后5页", active: PageIndex == nextTo, click: () => NavigateToPage(nextTo));
                            }
                            #endregion

                            #region 末页
                            GetPageItem(content, "末页", disabled: PageIndex == TotalPages, click: NavigateToLast);
                            #endregion

                        })
                        .Close();

    /// <summary>
    /// 计算分页页码的开始和结束的页码。
    /// </summary>
    /// <returns>开始和结束的页码。</returns>
    (int start, int end) ComputePageNumber()
    {
        var start = 0;
        var end = 0;

        var middle = PageNumber / 2;
        if (PageIndex <= middle)
        {
            start = 1;
            end = PageNumber;
        }
        else if (PageIndex > middle)
        {
            start = PageIndex - middle;
            end = PageIndex + middle;
        }
        if (end > TotalPages)
        {
            end = TotalPages;
            if (start + end != PageNumber - 2)
            {
                start = end - PageNumber + 2 - 1;
            }
        }
        if (end <= PageNumber)
        {
            start = 1;
        }

        return (start, end);
    }

    /// <summary>
    /// 跳转到指定页。
    /// </summary>
    /// <param name="page">要跳转的页码。</param>
    public async Task NavigateToPage(int page)
    {
        page = page < 1 ? 1 : page;
        page = page > TotalPages ? TotalPages : page;

        PageIndex = page;
        await OnPageChanged.InvokeAsync(page);
        await LoadDataSource();
        await this.Refresh();
    }
    /// <summary>
    /// 跳转到首页。
    /// </summary>
    public Task NavigateToFirst() => NavigateToPage(1);
    /// <summary>
    /// 跳转到末页。
    /// </summary>
    public Task NavigateToLast() => NavigateToPage(TotalPages);
    /// <summary>
    /// 跳转到上一页。
    /// </summary>
    public Task NavigateToPrevious() => NavigateToPage(--PageIndex);
    /// <summary>
    /// 跳转到下一页。
    /// </summary>
    public Task NavigateToNext() => NavigateToPage(++PageIndex);
}
