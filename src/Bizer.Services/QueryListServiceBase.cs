namespace Bizer.Services;
/// <summary>
/// 表示可以对数据进行列表查询的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TList">列表字段和过滤字段的类型。</typeparam>
public abstract class QueryListServiceBase<TContext, TEntity, TList>: QueryListServiceBase<TContext, TEntity, TList, TList>, IQueryListService<TList>
    where TContext : DbContext
    where TEntity : class
    where TList : class
{

}
/// <summary>
/// 表示可以对数据进行列表查询的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的类型。</typeparam>
public abstract class QueryListServiceBase<TContext, TEntity, TList, TListFilter> : ServiceBase<TContext, TEntity>, IQueryListService<TList, TListFilter>
    where TContext : DbContext
    where TEntity : class
    where TList : class
    where TListFilter : class
{
    #region GetList
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">列表检索的输入。</param>
    public virtual async Task<Returns<PagedInfo<TList>>> GetListAsync([Query] TListFilter? filter = default)
    {
        var query = QueryFilter(filter);

        query = ApplySkip(query, filter);
        query = ApplyTake(query, filter);
        query = ApplySorting(query);
        try
        {
            var data = await query.Select(m => Mapper.Map<TList>(m)).ToListAsync(CancellationToken);
            var total = await query.CountAsync();
            return Returns<PagedInfo<TList>>.Success(new(data, total));
        }
        catch (AggregateException ex)
        {
            return Returns<PagedInfo<TList>>.Failed("查询发生了错误，请查看日志").LogError(Logger, logMessage: ex.InnerExceptions.Select(m => m.Message).JoinAsString("；"), ex);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// 重写检索数据列表的过滤器。
    /// </summary>
    /// <param name="model">列表过滤器的输入模型。</param>
    protected virtual IQueryable<TEntity> QueryFilter(TListFilter? filter) => Query();



    /// <summary>
    /// 应用 Skip 数据查询筛选。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="filter">查询输入。</param>
    /// <returns></returns>
    protected IQueryable<TEntity> ApplySkip(IQueryable<TEntity> source, TListFilter? filter = default)
    {
        if (filter is null)
        {
            return source;
        }

        if (filter is IHasSkip skipInput && skipInput.Skip.HasValue)
        {
            source = source.Skip(skipInput.Skip.Value);
        }
        return source;
    }

    /// <summary>
    /// 应用 Take 数据查询筛选。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="filter">查询输入。</param>
    /// <returns></returns>
    protected IQueryable<TEntity> ApplyTake(IQueryable<TEntity> source, TListFilter? filter = default)
    {
        if (filter is null)
        {
            return source;
        }

        if (filter is IHasTake input && input.Take.HasValue)
        {
            source = source.Take(input.Take.Value);
        }
        return source;
    }
    /// <summary>
    /// 应用列表排序算法。
    /// </summary>
    /// <param name="source">要排序的数据源。</param>
    /// <returns>排序后的数据源。</returns>
    protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> source)
    {
        if (typeof(TEntity).IsAssignableTo(typeof(IHasCreateTime)))
        {
            return source.OrderByDescending(e => ((IHasCreateTime)e).CreateTime);
        }

        return source;
    }
    #endregion
}
