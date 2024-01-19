namespace Bizer.Extensions.ApplicationService.Abstractions;

/// <summary>
/// 提供列表查询的服务。
/// </summary>
/// <typeparam name="TList">列表字段和过滤的类型。</typeparam>
public interface IQueryListService<TList>:IQueryListService<TList,TList>
    where TList : class
{

}

/// <summary>
/// 提供列表查询的服务。
/// </summary>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public interface IQueryListService<TList, TListFilter>
    where TList : class
    where TListFilter : class
{
    /// <summary>
    /// 获取指定分页的结果列表。
    /// </summary>
    /// <param name="filter">获取列表的过滤输入模型。</param>
    /// <returns>一个获取分页结果的方法，返回 <see cref="Returns{TList}"/> 结果。</returns>
    [Get]
    Task<Returns<PagedInfo<TList>>> GetListAsync([Query] TListFilter? filter = default);
}
