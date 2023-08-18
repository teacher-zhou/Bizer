namespace Bizer.Services;

/// <summary>
/// 提供只读列表功能。
/// </summary>
public interface IReadOnlyService<TList,TListFilter> 
    where TList : class
    where TListFilter : class
{
    /// <summary>
    /// 获取指定分页的结果列表。
    /// </summary>
    /// <param name="filter">获取列表的过滤输入模型。</param>
    /// <returns>一个获取分页结果的方法，返回 <see cref="Returns{TList}"/> 结果。</returns>
    [Get]
    Task<Returns<PagedOutput<TList>>> GetListAsync([Query] TListFilter? filter = default);
}
