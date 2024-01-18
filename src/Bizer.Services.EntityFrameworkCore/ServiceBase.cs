using Mapster;

namespace Bizer.Services.EntityFrameworkCore;


/// <summary>
/// 提供操作 <see cref="TContext"/> 的服务基类。
/// </summary>
/// <typeparam name="TContext">数据上下文类型。</typeparam>
public abstract class ServiceBase<TContext> : ServiceBase, IDisposable
    where TContext : DbContext
{
    /// <summary>
    /// 获取映射对象。
    /// </summary>
    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

    /// <summary>
    /// 全局的 <see cref="IMapper"/> 对象的配置。
    /// </summary>
    protected TypeAdapterConfig GlobalTypeAdapterConfig => ServiceProvider.GetRequiredService<TypeAdapterConfig>();

    /// <summary>
    /// 获取数据库上下文。
    /// </summary>
    protected TContext Context
    {
        get
        {
            if ( ServiceProvider.TryGetService<IDbContextFactory<TContext>>(out var factory) )
            {
                return factory!.CreateDbContext();
            }
            return ServiceProvider.GetRequiredService<TContext>();
        }
    }

    /// <summary>
    /// 获取取消操作的令牌。默认是操作超过1分钟将引发异常。
    /// </summary>
    protected virtual CancellationToken CancellationToken
    {
        get
        {
            var source = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            return source.Token;
        }
    }

    #region Disaposable

    private bool _disposedValue;

    protected ServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    /// <param name="disposing"><c>true</c> 释放托管资源。</param>
    protected virtual void Dispose(bool disposing)
    {
        if ( !_disposedValue )
        {
            if ( disposing )
            {
                // TODO: dispose managed state (managed objects)
                Context?.Dispose();

                DisposeManagingObjects();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;

            DisposeObjects();
        }
    }

    /// <summary>
    /// 释放对象。
    /// </summary>
    protected virtual void DisposeObjects()
    {
    }

    /// <summary>
    /// 释放托管资源。
    /// </summary>
    protected virtual void DisposeManagingObjects()
    {
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~ServiceBase()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}

/// <summary>
/// 提供指定 <see cref="TContext"/> 数据库上下文并操作 <see cref="TEntity"/> 实体类型的服务基类。
/// </summary>
/// <typeparam name="TContext">数据上下文类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
public abstract class ServiceBase<TContext, TEntity> : ServiceBase<TContext>
    where TContext : DbContext
    where TEntity : class
{
    protected ServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// 获取数据集对象。
    /// </summary>
    protected DbSet<TEntity> Set() => Context.Set<TEntity>();

    /// <summary>
    /// 获取不追踪的可查询对象。
    /// </summary>
    protected virtual IQueryable<TEntity> Query() => Set().AsNoTracking();
}