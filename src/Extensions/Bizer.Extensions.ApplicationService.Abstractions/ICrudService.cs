namespace Bizer.Extensions.ApplicatonService.Abstractions;
/// <summary>
/// 提供 CRUD 的逻辑功能服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">CRUD 字段的模型类型。</typeparam>
public interface ICrudService<in TKey, TModel> : ICrudService<TKey, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TModel : class
{

}
/// <summary>
/// 提供 CRUD 的逻辑功能服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplayOrFilter">详情、列表和搜索显示字段的模型类型。</typeparam>
public interface ICrudService<in TKey, TCreateOrUpdate, TDisplayOrFilter> : ICrudService<TKey, TCreateOrUpdate, TDisplayOrFilter, TDisplayOrFilter>
    where TKey : IEquatable<TKey>
    where TCreateOrUpdate : class
    where TDisplayOrFilter : class
{

}
/// <summary>
/// 提供 CRUD 的逻辑功能服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplay">详情和列表显示字段的模型类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public interface ICrudService<in TKey, TCreateOrUpdate, TDisplay, TListFilter> : ICrudService<TKey, TCreateOrUpdate, TDisplay, TDisplay, TListFilter>
    where TKey : IEquatable<TKey>
    where TCreateOrUpdate : class
    where TDisplay : class
    where TListFilter : class
{

}

/// <summary>
/// 提供 CRUD 的逻辑功能服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public interface ICrudService<in TKey, TCreateOrUpdate, TDetail, TList, TListFilter> : ICrudService<TKey, TCreateOrUpdate, TCreateOrUpdate, TDetail, TList, TListFilter>
    where TKey : IEquatable<TKey>
    where TCreateOrUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{

}
/// <summary>
/// 提供 CRUD 的逻辑功能服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TCreate">创建字段的模型类型。</typeparam>
/// <typeparam name="TUpdate">更新字段的模型类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public interface ICrudService<in TKey, TCreate, TUpdate, TDetail, TList, TListFilter> : IQueryService<TKey, TDetail, TList, TListFilter>
    where TKey : IEquatable<TKey>
    where TCreate : class
    where TUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{
    /// <summary>
    /// 创建指定的输入模型对象。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns>一个创建方法，返回 <see cref="Returns"/> 结果。</returns>
    [Post]
    Task<Returns<TDetail?>> CreateAsync([Body] TCreate model);
    /// <summary>
    /// 更新指定 id 的输入模型对象。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的输入模型。</param>
    /// <returns>一个更新方法，返回 <see cref="Returns"/> 结果。</returns>
    [Put("{id}")]
    Task<Returns<TDetail?>> UpdateAsync([Path] TKey id, [Body] TUpdate model);
    /// <summary>
    /// 删除指定的 id。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    /// <returns>一个删除方法，返回 <see cref="Returns"/> 结果。</returns>
    [Delete("{id}")]
    Task<Returns<TDetail?>> DeleteAsync([Path] TKey id);
}