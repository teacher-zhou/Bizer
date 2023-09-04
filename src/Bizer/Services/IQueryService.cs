namespace Bizer.Services;
/// <summary>
/// 提供查询的服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TList">列表字段、过滤字段以及详情字段的类型。</typeparam>
public interface IQueryService<in TKey, TList> : IQueryService<TKey, TList, TList>
    where TKey : IEquatable<TKey>
    where TList : class
{

}

/// <summary>
/// 提供查询的服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段和过滤字段的类型。</typeparam>
public interface IQueryService<in TKey, TDetail, TList> : IQueryService<TKey, TDetail, TList,TList>, IQueryListService<TList>
    where TKey : IEquatable<TKey>
    where TDetail : class
    where TList : class
{

}
/// <summary>
/// 提供查询的服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public interface IQueryService<in TKey, TDetail, TList, TListFilter> : IQueryListService<TList, TListFilter>
where TKey : IEquatable<TKey>
where TDetail : class
where TList : class
where TListFilter : class
{
    /// <summary>
    /// 获取指定 id 的结果。
    /// </summary>
    /// <param name="id">要获取的 id 。</param>
    /// <returns>一个获取结果的方法，返回 <see cref="Returns{TDetail}"/> 结果。</returns>
    [Get("{id}")]
    Task<Returns<TDetail?>> GetAsync([Path] TKey id);
}