namespace Bizer.Services.Models;

/// <summary>
/// 表示分页输入。
/// </summary>
/// <param name="Page">获取或设置当前页码。默认是 1。</param>
/// <param name="Size">获取或设置每一页的数据量。默认是 10。</param>
public record class PagedInput(int Page = 1, int Size = 10) : IHasSkipInput, IHasTakeInput
{
    int? IHasSkipInput.Skip => (Page - 1) * Size;
    int? IHasTakeInput.Take => Page * Size;
}
