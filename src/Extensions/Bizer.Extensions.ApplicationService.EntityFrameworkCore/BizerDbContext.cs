using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace Bizer.Extensions.ApplicatonService.EntityFrameworkCore;
/// <summary>
/// 实现了自动化配置的 <see cref="DbContext"/> 类。
/// </summary>
public class BizerDbContext : DbContext
{
    private readonly DbContextConfigureOptions _options;

    /// <summary>
    /// 初始化 <see cref="BizerDbContext"/> 类的新实例。
    /// </summary>
    /// <param name="serviceProvider"></param>
    public BizerDbContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        //this._options = options;
    }

    /// <summary>
    /// 获取服务。
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取自动发现配置。
    /// </summary>
    public AutoDiscoveryOptions AutoDiscoveryOptions => ServiceProvider.GetRequiredService<AutoDiscoveryOptions>();

    /// <summary>
    /// 从 <see cref="DbContextConfigureOptions"/> 配置数据库引擎模块。
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ServiceProvider.GetRequiredService<DbContextConfigureOptions>().ConfigureOptionBuilder?.Invoke(optionsBuilder);
    }

    /// <summary>
    /// 从自动发现配置中自动配置 实现了 <see cref="IEntityTypeConfiguration{TEntity}"/> 的实体配置。
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach ( var assembly in AutoDiscoveryOptions.GetDiscoveredAssemblies() )
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
