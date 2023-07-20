using Bizer.Services.Models;
using Mapster;
using System.Reflection;

namespace Bizer.Services;
/// <summary>
/// 表示 CRUD 服务的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
public abstract class CrudServiceBase<TContext, TKey, TEntity> : CrudServiceBase<TContext, TKey, TEntity, TEntity>, ICrudService<TKey, TEntity>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
/// 表示 CRUD 服务的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TModel">CURD字段的模型类型。</typeparam>
public abstract class CrudServiceBase<TContext, TKey, TEntity, TModel> : CrudServiceBase<TContext, TKey, TEntity, TModel, TModel>, ICrudService<TKey, TModel>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TModel : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity, TModel}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
/// 表示 CRUD 服务的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplay">详情、列表和过滤字段的模型类型。</typeparam>
public abstract class CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TDisplay> : CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TDisplay, TDisplay>, ICrudService<TKey, TCreateOrUpdate, TDisplay>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDisplay : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity, TCreateOrUpdate, TDisplay}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示 CRUD 服务的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplay">详情和列表字段的模型类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public abstract class CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TDisplay, TListFilter> : CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TCreateOrUpdate, TDisplay, TDisplay, TListFilter>, ICrudService<TKey, TCreateOrUpdate, TDisplay, TListFilter>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDisplay : class
    where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity, TCreateOrUpdate, TDisplay, TListFilter}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示 CRUD 服务的基类。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public abstract class CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TDetail, TList, TListFilter> : CrudServiceBase<TContext, TKey, TEntity, TCreateOrUpdate, TCreateOrUpdate, TDetail, TList, TListFilter>, ICrudService<TKey, TCreateOrUpdate, TDetail, TList, TListFilter>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity, TCreateOrUpdate, TDetail, TList, TListFilter}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

    /// <summary>
    /// 表示 CRUD 服务的基类。
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <typeparam name="TCreate">创建字段的模型类型。</typeparam>
    /// <typeparam name="TUpdate">更新字段的模型类型。</typeparam>
    /// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
    /// <typeparam name="TList">列表字段的类型。</typeparam>
    /// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
    public abstract class CrudServiceBase<TContext, TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter> : ServiceBase<TContext,TEntity>, ICrudService<TKey, TCreate, TUpdate, TDetail, TList, TListFilter>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreate : class
    where TUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TContext, TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter}"/> 类的新实例。
    /// </summary>
    protected CrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }


    #region Create
    /// <summary>
    /// 创建指定数据。
    /// </summary>
    /// <param name="model">要创建的对象。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async Task<Returns<TDetail?>> CreateAsync([Body] TCreate model)
    {
        if ( model is null )
        {
            throw new ArgumentNullException(nameof(model));
        }

        if ( !Validator.TryValidate(model, out var errors) )
        {
            return Returns<TDetail?>.Failed(errors.ToArray()).LogError(Logger);
        }

        var entity = Mapper.Map<TEntity>(model);
        Set().Add(entity);
        return await SaveChangesAsync(() => Mapper?.Map<TDetail?>(entity));
    }
    #endregion

    #region Update
    /// <summary>
    /// 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 Id。</param>
    /// <param name="model">要更新的字段。</param>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> 是 null。</exception>
    public virtual async Task<Returns<TDetail?>> UpdateAsync([Path] TKey id, [Body] TUpdate model)
    {
        if ( model is null )
        {
            throw new ArgumentNullException(nameof(model));
        }

        if ( !Validator.TryValidate(model, out var errors) )
        {
            return Returns<TDetail?>.Failed(errors.ToArray()).LogError(Logger);
        }


        var entity = await Context.FindAsync<TEntity>(id);
        if ( entity is null )
        {
            return Returns<TDetail?>.Failed($"实体 id({id}) 未找到").LogError(Logger);
        }

        var config= BuildUpdateTypeAdapterConfig();

        entity = model.Adapt(entity, config);
        Context.Update(entity);

        return await SaveChangesAsync(() => MapToDetail(entity));
    }
    #endregion

    #region Delete
    /// <summary>
    /// 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 Id。</param>
    public virtual async Task<Returns<TDetail?>> DeleteAsync([Path] TKey id)
    {
        var entity = await FindAsync(id);
        if ( entity is null )
        {
            return Returns<TDetail?>.Failed($"实体 id({id}) 未找到").LogError(Logger);
        }

        Set().Remove(entity);

        return await SaveChangesAsync(() => Mapper?.Map<TDetail?>(entity));
    }
    #endregion

    #region Get
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id">要获取的 Id。</param>
    public virtual async Task<Returns<TDetail?>> GetAsync([Path] TKey id)
    {
        var entity = await FindAsync(id);
        if ( entity is null )
        {
            return Returns<TDetail?>.Failed($"实体 id({id}) 未找到").LogError(Logger);
        }
        var detail = MapToDetail(entity);
        return Returns<TDetail?>.Success(detail);
    }
    #endregion

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
            var data = await query.Select(m => Mapper.Map<TList>(m)).ToListAsync();
            var total = await query.CountAsync();
            return Returns<PagedOutput<TList>>.Success(new(data, total));
        }
        catch ( AggregateException ex )
        {
            return Returns<PagedOutput<TList>>.Failed().LogError(Logger, ex);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// 重写检索数据列表的过滤器。
    /// </summary>
    /// <param name="model">列表过滤器的输入模型。</param>
    protected virtual IQueryable<TEntity> QueryFilter(TListFilter? filter) => Query();
    protected virtual ValueTask<TEntity?> FindAsync(TKey id) => Context.FindAsync<TEntity>(id);

    /// <summary>
    /// 应用 Skip 数据查询筛选。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="filter">查询输入。</param>
    /// <returns></returns>
    protected IQueryable<TEntity> ApplySkip(IQueryable<TEntity> source, TListFilter? filter = default)
    {
        if ( filter is null )
        {
            return source;
        }

        if ( filter is IHasSkipInput skipInput && skipInput.Skip.HasValue )
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
        if ( filter is null )
        {
            return source;
        }

        if ( filter is IHasTakeInput input && input.Take.HasValue )
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
        if ( typeof(TEntity).IsAssignableTo(typeof(IHasId<TKey>)) )
        {
            return source.OrderByDescending(e => ((IHasId<TKey>)e).Id);
        }

        return source;
    }

    /// <summary>
    /// 保存记录。已经捕获了异常，根据返回结果判断。
    /// </summary>
    /// <typeparam name="TResult">返回的结果。</typeparam>
    /// <param name="afterSaveChangesSuccessully">保存成功后执行的委托。</param>
    /// <returns></returns>
    protected virtual async Task<Returns<TResult?>> SaveChangesAsync<TResult>(Func<TResult> afterSaveChangesSuccessully)
    {
        try
        {
            var rows = await Context.SaveChangesAsync();
            if ( rows == 0 )
            {
                return Returns<TResult?>.Failed("影响结果是0").LogError(Logger);
            }
            Logger?.LogInformation($"影响结果数量:{rows}");
            return Returns<TResult?>.Success(afterSaveChangesSuccessully());
        }
        catch ( DbUpdateConcurrencyException ex )
        {
            return Returns<TResult?>.Failed("并发操作异常").LogError(Logger, ex);
        }
        catch ( DbUpdateException ex )
        {
            return Returns<TResult?>.Failed("数据库更新异常").LogError(Logger, ex);
        }
        catch ( InvalidOperationException ex )
        {
            return Returns<TResult?>.Failed("数据库操作异常").LogError(Logger, ex);
        }
        catch ( Exception ex )
        {
            return Returns<TResult?>.Failed("SaveChangesAsync Exception").LogError(Logger, ex);
        }
    }

    /// <summary>
    /// 将 <typeparamref name="TCreate"/> 映射到 <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="model">要映射的输入模型。</param>
    /// <returns>映射成功的实体。</returns>
    protected virtual TDetail? MapToDetail(TEntity? entity)
    {
        Checker.NotNull(entity, nameof(entity));

        if ( typeof(TDetail) != typeof(TEntity) )
        {
            return Mapper?.Map<TDetail?>(entity!);
        }
        return entity as TDetail;
    }

    /// <summary>
    /// 重写构建更新时的映射逻辑。
    /// <para>
    /// 默认忽略 Id 属性的映射关系。
    /// </para>
    /// </summary>
    protected virtual TypeAdapterConfig BuildUpdateTypeAdapterConfig()
        => TypeAdapterConfig<TUpdate, TEntity>.NewConfig().IgnoreMember((s, t) => s.Name == "Id" && s.Info is PropertyInfo).Config;
    #endregion
}