using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Stores.EntityFrameworkCore;

/// <summary>
/// 添加 EF 存储的构造器。
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class BizerEfCoreStoreBuilder<TContext>
    where TContext : DbContext, IUnitOfWork
{
    private readonly BizerStoreBuilder _builder;

    internal BizerEfCoreStoreBuilder(BizerStoreBuilder builder) => _builder = builder;

    /// <summary>
    /// 获取服务。
    /// </summary>
    public IServiceCollection Services => _builder.Services;
    /// <summary>
    /// 添加 DbContext 的服务并将其作为工作单元。
    /// </summary>
    /// <param name="optionsAction">配置 DbContext 的委托。</param>
    public BizerEfCoreStoreBuilder<TContext> AddDbContext(Action<DbContextOptionsBuilder>? optionsAction = default)
    {
        Services.AddDbContext<TContext>(optionsAction);
        AddUnitOfWork();
        return this;
    }
    /// <summary>
    /// 注册工厂而不是直接注册上下文类型可以方便地创建新实例。对于Blazor应用程序和其他依赖注入范围与上下文生命周期不一致的情况，建议注册工厂。
    /// </summary>
    /// <param name="optionsAction">配置 DbContext 的委托。</param>
    /// <returns></returns>
    public BizerEfCoreStoreBuilder<TContext> AddDbContextFactory(Action<DbContextOptionsBuilder>? optionsAction = default)
    {
        Services.AddDbContextFactory<TContext>(optionsAction);
        AddUnitOfWork();
        return this;
    }
    /// <summary>
    /// DbContext池可以通过重用上下文实例来提高高吞吐量场景中的性能。但是，对于大多数应用程序，这种性能增益非常小。注意，当使用池时，上下文配置不能在不同的使用之间改变，并且注入到上下文的有作用域的服务只会从初始作用域解析一次。只有当性能测试表明DbContext池提供了真正的提升时才考虑使用它。
    /// </summary>
    /// <param name="optionAction">配置 DbContext 的委托。</param>
    /// <param name="poolSize">设置池保留的最大实例数。默认为1024。</param>
    /// <returns></returns>
    public BizerEfCoreStoreBuilder<TContext> AddDbContextPool(Action<DbContextOptionsBuilder> optionsAction, int poolSize = 1024)
    {
        Services.AddDbContextPool<TContext>(optionsAction, poolSize);
        AddUnitOfWork();
        return this;
    }

    /// <summary>
    /// 使当前的 <typeparamref name="TContext"/> 成为工作单元。
    /// </summary>
    /// <returns></returns>
    public BizerEfCoreStoreBuilder<TContext> AddUnitOfWork()
    {
        _builder.AddUnitOfWork<TContext>();
        return this;
    }

    /// <summary>
    /// 添加通用的仓储服务。
    /// <para>
    /// 如果使用 <see cref="AddDbContext(Action{DbContextOptionsBuilder}?)"/> 方法，则需要使用这种模式添加仓储服务。
    /// </para>
    /// <para>
    /// 示例代码：
    /// <code language="cs">
    /// public class Example
    /// {
    ///     private readonly IRepository&lt;Entity> _repository;
    ///     public Example(IRepository&lt;Entity> repository)
    ///     {
    ///         _repository = repository;
    ///     }
    ///     ...
    /// }
    /// </code>
    /// <code language="cs">
    /// public class Example
    /// {
    ///     private readonly IEFCoreRepository&lt;Entity> _repository;
    ///     public Example(IEFCoreRepository&lt;Entity> repository)
    ///     {
    ///         _repository = repository;
    ///     }
    ///     ...
    /// }
    /// </code>
    /// 可以显示地调用指定的 DbContext 类型的仓储：
    /// <code language="cs">
    /// public class Example
    /// {
    ///     private readonly IEFCoreRepository&lt;MyDbContext, Entity> _repository;
    ///     public Example(IEFCoreRepository&lt;MyDbContext, Entity> repository)
    ///     {
    ///         _repository = repository;
    ///     }
    ///     ...
    /// }
    /// </code>
    /// </para>
    /// </summary>
    public BizerEfCoreStoreBuilder<TContext> AddDefaultRepository()
    {
        _builder.AddDefaultRepository(typeof(EFCoreRepository<>));
        _builder.AddRepository(typeof(IEFCoreRepository<>), typeof(EFCoreRepository<>));
        _builder.AddRepository(typeof(IEFCoreRepository<,>), typeof(EFCoreRepository<,>));
        return this;
    }
}
