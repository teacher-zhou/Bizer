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
    public abstract class CrudServiceBase<TContext, TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter> : QueryServiceBase<TContext,TEntity,TKey,TDetail,TList,TListFilter>, ICrudService<TKey, TCreate, TUpdate, TDetail, TList, TListFilter>, IDisposable
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreate : class
    where TUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{


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

    /// <summary>
    /// 保存记录。已经捕获了异常，根据返回结果判断。
    /// </summary>
    /// <typeparam name="TResult">返回的结果。</typeparam>
    /// <param name="afterSaveChangesSuccessully">保存成功后执行的委托。</param>
    /// <returns></returns>
    protected virtual async Task<Returns<TResult?>> SaveChangesAsync<TResult>(Func<TResult> afterSaveChangesSuccessully)
    {
        var failedMessage = "数据在保存时遇到了错误，请查看日志";
        try
        {
            var rows = await Context.SaveChangesAsync(CancellationToken);
            if ( rows == 0 )
            {
                return Returns<TResult?>.Failed(failedMessage).LogError(Logger,$"调用 {nameof(DbContext.SaveChangesAsync)} 后的结果返回的是：{rows}");
            }
            return Returns<TResult?>.Success(afterSaveChangesSuccessully()).LogInfo(Logger, $"调用 {nameof(DbContext.SaveChangesAsync)} 后的结果返回的是：{rows}");
        }
        catch ( Exception ex )
        {
            return Returns<TResult?>.Failed(failedMessage).LogError(Logger, exception: ex);
        }
    }


    /// <summary>
    /// 重写构建更新时的映射逻辑。
    /// <para>
    /// 默认忽略 Id 属性的映射关系。
    /// </para>
    /// </summary>
    protected virtual TypeAdapterConfig BuildUpdateTypeAdapterConfig()
        => TypeAdapterConfig<TUpdate, TEntity>.NewConfig().IgnoreMember((s, t) => s.Name == "Id" && s.Info is PropertyInfo).Config;
}