namespace Bizer.Stores;
/// <summary>
/// 表示实现仓储的基类。
/// </summary>
/// <typeparam name="TEntity">实体的类型。</typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="Repository{TEntity}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    protected Repository(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

    /// <inheritdoc/>
    public abstract IQueryable<TEntity> Query { get; }
    /// <inheritdoc/>
    protected IServiceProvider ServiceProvider { get; }

    /// <inheritdoc/>
    public abstract TEntity Add(TEntity entity);
    /// <inheritdoc/>
    public abstract TEntity? Modify(TEntity entity);
    /// <inheritdoc/>
    public abstract TEntity Remove(TEntity entity);
    /// <inheritdoc/>
    public abstract ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
}
