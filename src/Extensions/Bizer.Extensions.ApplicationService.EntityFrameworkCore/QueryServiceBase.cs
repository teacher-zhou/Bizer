namespace Bizer.Extensions.ApplicatonService.EntityFrameworkCore;

public abstract class QueryServiceBase<TContext, TEntity, TKey, TList> : QueryServiceBase<TContext, TEntity, TKey, TList, TList>, IQueryService<TKey, TList>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TList : class
{
    protected QueryServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class QueryServiceBase<TContext, TEntity, TKey, TDetail, TList> : QueryServiceBase<TContext, TEntity, TKey, TDetail, TList, TList>, IQueryService<TKey, TDetail, TList>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TDetail : class
    where TList : class
{
    protected QueryServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示可以对数据进行查询的服务基类。
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDetail"></typeparam>
/// <typeparam name="TList"></typeparam>
/// <typeparam name="TListFilter"></typeparam>
public abstract class QueryServiceBase<TContext, TEntity, TKey, TDetail, TList, TListFilter> : QueryListServiceBase<TContext, TEntity, TList, TListFilter>, IQueryService<TKey, TDetail, TList, TListFilter>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{
    protected QueryServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    #region Get
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要获取的 Id。</param>
    public virtual async Task<Returns<TDetail?>> GetAsync([Path] TKey id)
    {
        var entity = await FindAsync(id);
        if (entity is null)
        {
            return Returns<TDetail?>.Failed($"实体 id({id}) 未找到").LogError(Logger);
        }
        var detail = MapToDetail(entity);
        return Returns<TDetail?>.Success(detail);
    }
    #endregion

    /// <summary>
    /// 查询指定 id 的实体。
    /// </summary>
    /// <param name="id">要查询的 id 主键。</param>
    /// <returns>查询到的实体或 <c>null</c>。</returns>
    protected virtual ValueTask<TEntity?> FindAsync(TKey id) => Context.FindAsync<TEntity>(new object[] { id }, CancellationToken);


    /// <summary>
    /// 将 <typeparamref name="TCreate"/> 映射到 <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="model">要映射的输入模型。</param>
    /// <returns>映射成功的实体。</returns>
    protected virtual TDetail? MapToDetail(TEntity? entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (typeof(TDetail) != typeof(TEntity))
        {
            return Mapper?.Map<TDetail?>(entity!);
        }
        return entity as TDetail;
    }
}
