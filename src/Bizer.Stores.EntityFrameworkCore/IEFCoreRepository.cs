using Microsoft.EntityFrameworkCore;

namespace Bizer.Stores.EntityFrameworkCore;

/// <summary>
/// 提供对 EF Core 操作 <see cref="BizerDbContext"/> 的仓储功能。
/// </summary>
/// <typeparam name="TEntity">实体类型。</typeparam>
public interface IEFCoreRepository<TEntity> : IEFCoreRepository<BizerDbContext, TEntity>
    where TEntity : class
{

}

/// <summary>
/// 提供对 EF Core 操作指定的数据库上下文的仓储功能。
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
public interface IEFCoreRepository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : class
{
    /// <summary>
    /// 获取操作的上下文。
    /// </summary>
    TContext Context { get; }
}
