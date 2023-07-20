using Mapster;

namespace Bizer.Services;

/// <summary>
/// 表示基本的业务服务的基类。
/// <para>
/// 这是一个空的基类，提供了一些基本的接口。
/// </para>
/// </summary>
public abstract class ServiceBase
{
    protected ServiceBase(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 获取日志对象。
    /// </summary>
    protected ILogger? Logger => ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(this.GetType().Name);

    /// <summary>
    /// 获取映射对象。
    /// </summary>
    protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

    /// <summary>
    /// 全局的 <see cref="IMapper"/> 对象的配置。
    /// </summary>
    protected TypeAdapterConfig GlobalTypeAdapterConfig => ServiceProvider.GetRequiredService<TypeAdapterConfig>();
}

public abstract class ServiceBase<TContext> : ServiceBase,IDisposable
    where TContext : DbContext
{
    protected ServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

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

    #region Disaposable

    private bool _disposedValue;
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

public abstract class ServiceBase<TContext, TEntity> : ServiceBase<TContext>
    where TContext : DbContext
    where TEntity : class
{
    protected ServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected DbSet<TEntity> Set() => Context.Set<TEntity>();

    protected IQueryable<TEntity> Query() => Set().AsNoTracking();
}