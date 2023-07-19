using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Stores.EntityFrameworkCore;

/// <summary>
/// 内置的 <see cref="DbContext"/>。可以根据自动发现动态添加 <see cref="DbSet{TEntity}"/> 对象。
/// </summary>
public class BizerDbContext : DbContext ,IUnitOfWork
{
    /// <summary>
    /// 初始化 <see cref="BizerDbContext"/> 类的新实例。
    /// </summary>
    /// <param name="options"></param>
    /// <param name="serviceProvider"></param>
    public BizerDbContext(DbContextOptions options,IServiceProvider serviceProvider) : base(options)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// 初始化 <see cref="BizerDbContext"/> 类的新实例。
    /// </summary>
    protected BizerDbContext(IServiceProvider serviceProvider):base()
    {
        ServiceProvider = serviceProvider;
    }
    /// <summary>
    /// <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取注册发现的配置。
    /// </summary>
    protected AutoDiscoveryOptions AutoDiscoveryOptions => ServiceProvider.GetRequiredService<AutoDiscoveryOptions>();

    /// <inheritdoc/>
    public virtual int Commit() => SaveChanges();

    /// <inheritdoc/>
    public virtual Task<int> CommitAsync(CancellationToken cancellationToken = default) => SaveChangesAsync(cancellationToken);

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach ( var assembly in AutoDiscoveryOptions.GetDiscoveredAssemblies() )
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
