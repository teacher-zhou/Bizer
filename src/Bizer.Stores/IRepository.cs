namespace Bizer.Stores;

/// <summary>
/// 提供简易仓储的功能。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 获取可查询的实体。
    /// </summary>
    IQueryable<TEntity> Query { get; }
    /// <summary>
    /// 向仓储中添加指定的实体对象。
    /// </summary>
    /// <param name="entity">要添加的实体。</param>
    /// <returns>要添加的实体或 <c>null</c>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> 是 <c>null</c>。</exception>
    TEntity? Add(TEntity entity);
    /// <summary>
    /// 从仓储中移除指定的实体对象。
    /// </summary>
    /// <param name="entity">要移除的实体。</param>
    /// <returns>要删除的实体或 <c>null</c>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> 是 <c>null</c>。</exception>
    TEntity? Remove(TEntity entity);
    /// <summary>
    /// 更新仓储中指定的实体对象。
    /// </summary>
    /// <param name="entity">更新的实体。</param>
    /// <returns>要修改的实体或 <c>null</c>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> 是 <c>null</c>。</exception>
    TEntity? Modify(TEntity entity);
    /// <summary>
    /// 检索指定主键的实体。
    /// </summary>
    /// <param name="keyValues">主键数组。</param>
    /// <returns>包含实体的异步任务，任务结束后返回实体对象或 <c>null</c>。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="keyValues"/> 是 <c>null</c>。</exception>
    ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
}