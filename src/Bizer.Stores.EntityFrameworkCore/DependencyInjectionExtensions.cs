using Microsoft.EntityFrameworkCore;

namespace Bizer.Stores.EntityFrameworkCore;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加 EF Core 的存储并确定 <typeparamref name="TContext"/> 类型。
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerEfCoreStoreBuilder<TContext> AddEFCoreStore<TContext>(this BizerStoreBuilder builder) where TContext : DbContext, IUnitOfWork
    {
        var efBuilder = new BizerEfCoreStoreBuilder<TContext>(builder);
        return efBuilder;
    }

    /// <summary>
    /// 添加 EF Core 的存储并使用 <see cref="BizerDbContext"/> 作为默认的数据库上下文。
    /// </summary>
    public static BizerEfCoreStoreBuilder<BizerDbContext> AddEFCoreStore(this BizerStoreBuilder builder)
        =>builder.AddEFCoreStore<BizerDbContext>();

    ///// <summary>
    ///// 添加 DbContext 的服务并将其作为工作单元。
    ///// </summary>
    ///// <typeparam name="TContext">DbContext 的类型。</typeparam>
    ///// <param name="builder"></param>
    ///// <param name="optionAction">配置 DbContext 的委托。</param>
    ///// <returns></returns>
    //public static BizerStoreBuilder AddDbContext<TContext>(this BizerStoreBuilder builder, Action<DbContextOptionsBuilder>? optionAction = default)
    //    where TContext : DbContext, IUnitOfWork
    //{
    //    builder.Services.AddDbContext<TContext>(optionAction);
    //    return builder;
    //}

    ///// <summary>
    ///// 使用 <see cref="BizerDbContext"/> 作为 DbContext 的服务。
    ///// </summary>
    ///// <param name="builder"></param>
    ///// <param name="optionAction">配置 DbContext 的委托。</param>
    ///// <returns></returns>
    //public static BizerStoreBuilder AddDbContext(this BizerStoreBuilder builder, Action<DbContextOptionsBuilder>? optionAction = default)
    //    => builder.AddDbContext<BizerDbContext>(optionAction);

    //public static BizerStoreBuilder AddDbContextFactory<TContext>(this BizerStoreBuilder builder, Action<DbContextOptionsBuilder>? optionAction = default)
    //    where TContext : DbContext
    //{
    //    builder.Services.AddDbContextFactory<TContext>(optionAction);
    //    return builder;
    //}

    //public static BizerStoreBuilder AddDbContextFactory(this BizerStoreBuilder builder, Action<DbContextOptionsBuilder>? optionAction = default)
    //    => builder.AddDbContextFactory<BizerDbContext>(optionAction);

    ///// <summary>
    ///// 添加通用的仓储服务。
    ///// <para>
    ///// 如果使用 <see cref="AddDbContext(BizerStoreBuilder, Action{DbContextOptionsBuilder}?)"/> 方法，则需要使用这种模式添加仓储服务。
    ///// </para>
    ///// <para>
    ///// 示例代码：
    ///// <code language="cs">
    ///// public class Example
    ///// {
    /////     private readonly IRepository&lt;Entity> _repository;
    /////     public Example(IRepository&lt;Entity> repository)
    /////     {
    /////         _repository = repository;
    /////     }
    /////     ...
    ///// }
    ///// </code>
    ///// <code language="cs">
    ///// public class Example
    ///// {
    /////     private readonly IEFCoreRepository&lt;Entity> _repository;
    /////     public Example(IEFCoreRepository&lt;Entity> repository)
    /////     {
    /////         _repository = repository;
    /////     }
    /////     ...
    ///// }
    ///// </code>
    ///// </para>
    ///// </summary>
    //public static BizerStoreBuilder AddDefaultRepository(this BizerStoreBuilder builder)
    //{
    //    builder.AddDefaultRepository(typeof(EFCoreRepository<>));
    //    builder.AddRepository(typeof(IEFCoreRepository<>), typeof(EFCoreRepository<>));
    //    return builder;
    //}
    ///// <summary>
    ///// 添加具备自定义 DbContext 的仓储服务。
    ///// <para>
    ///// 如果使用了 <see cref="AddDbContext{TContext}(BizerStoreBuilder, Action{DbContextOptionsBuilder}?)"/> 方法，则需要使用这种模式添加仓储服务。
    ///// </para>
    ///// <para>
    ///// 示例代码：
    ///// <code language="cs">
    ///// public class Example
    ///// {
    /////     private readonly IEFCoreRepository&lt;MyDbContext, Entity> _repository;
    /////     public Example(IEFCoreRepository&lt;MyDbContext, Entity> repository)
    /////     {
    /////         _repository = repository;
    /////     }
    /////     ...
    ///// }
    ///// </code>
    ///// </para>
    ///// </summary>
    //public static BizerStoreBuilder AddDefaultEfCoreRepository(this BizerStoreBuilder builder)
    //{
    //    builder.Services.AddScoped(typeof(IEFCoreRepository<,>), typeof(EFCoreRepository<,>));
    //    return builder;
    //}
}
