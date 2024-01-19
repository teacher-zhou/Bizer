namespace Bizer.Extensions.ApplicatonService.Abstractions;

/// <summary>
/// 具备分页请求的传输对象。
/// <para>
/// 当继承该类，则会自动对列表进行分页过滤的查询。
/// </para>
/// </summary>
/// <param name="Page">获取或设置当前页码。默认是 1。</param>
/// <param name="Size">获取或设置每一页的数据量。默认是 10。</param>
public record class PagedDto(int Page = 1, int Size = 10) : IHasSkip, IHasTake
{
    int? IHasSkip.Skip => (Page - 1) * Size;
    int? IHasTake.Take => Page * Size;
}
