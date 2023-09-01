namespace Bizer.Services;

public abstract class QueryListServiceBase<TContext,TKey, TEntity, TList, TListFilter>:ServiceBase<TContext,TEntity>,IQueryListService<TList,TListFilter>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TList : class
    where TListFilter : class
{
    protected QueryListServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    #region GetList
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model">列表检索的输入。</param>
    public virtual async Task<Returns<PagedOutput<TList>>> GetListAsync([Query] TListFilter? filter = default)
    {
        var query = QueryFilter(filter);

        query = ApplySkip(query, filter);
        query = ApplyTake(query, filter);
        query = ApplySorting(query);
        try
        {
            var data = await query.Select(m => Mapper.Map<TList>(m)).ToListAsync(CancellationToken);
            var total = await query.CountAsync();
            return Returns<PagedOutput<TList>>.Success(new(data, total));
        }
        catch (AggregateException ex)
        {
            return Returns<PagedOutput<TList>>.Failed("查询发生了错误，请查看日志").LogError(Logger, logMessage: ex.InnerExceptions.Select(m => m.Message).JoinAsString("；"), ex);
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

        if (filter is IHasSkipInput skipInput && skipInput.Skip.HasValue)
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

        if (filter is IHasTakeInput input && input.Take.HasValue)
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
        if (typeof(TEntity).IsAssignableTo(typeof(IHasId<>)))
        {
            return source.OrderByDescending(e => ((IHasId<TKey>)e).Id);
        }

        return source;
    }
    #endregion
}
