using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Stores.EntityFrameworkCore;
/// <summary>
/// 默认使用 <see cref="BizerDbContext"/> 上下文的仓储。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
public class EFCoreRepository<TEntity> : EFCoreRepository<BizerDbContext, TEntity>, IEFCoreRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// 初始化 <see cref="EFCoreRepository{TEntity}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    public EFCoreRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 表示 EF Core 的指定数据库上下文的仓储实现。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
public class EFCoreRepository<TContext, TEntity> : Repository<TEntity>, IEFCoreRepository<TContext, TEntity>
    where TEntity : class
    where TContext : DbContext
{
    /// <summary>
    /// 初始化 <see cref="EFCoreRepository{TContext, TEntity}"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider">服务提供器。</param>
    public EFCoreRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取非跟踪状态的可查询实体。
    /// </summary>
    public override IQueryable<TEntity> Query => Context.Set<TEntity>().AsNoTracking();

    
    /// <inheritdoc/>
    public TContext Context => CreateContext();

    TContext CreateContext()
    {
        if(ServiceProvider.TryGetService<IDbContextFactory<TContext>>(out var contextFactory) )
        {
            return contextFactory!.CreateDbContext();
        }
        return ServiceProvider.GetRequiredService<TContext>();
    }

    /// <inheritdoc/>
    public override TEntity Add(TEntity entity)
    {
        if ( entity is null )
        {
            throw new ArgumentNullException(nameof(entity));
        }
        Context.Add(entity);
        return entity;
    }

    /// <inheritdoc/>
    public override ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => Context.FindAsync<TEntity>(keyValues);

    /// <inheritdoc/>
    public override TEntity Remove(TEntity entity)
    {
        if ( entity is null )
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Context.Remove(entity);
        return entity;
    }

    /// <inheritdoc/>
    public override TEntity? Modify(TEntity entity)
    {
        if ( entity is null )
        {
            throw new ArgumentNullException(nameof(entity));
        }
        if ( Context.Entry(entity).State == EntityState.Detached )
        {
            Context.Attach(entity);
        }
        return entity;
    }
}