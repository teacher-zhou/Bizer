using Mapster;
namespace Bizer.Services;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加映射的服务，后续可以使用 <see cref="IMapper"/> 对象操作映射。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddMapper(this BizerBuilder builder, Action<TypeAdapterConfig>? configure = default)
    {
        TypeAdapterConfig config = new();
        configure?.Invoke(config);
        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();
        return builder;
    }

    /// <summary>
    /// 添加继承 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 服务。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContext<TContext>(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions) where TContext : BizerDbContext
    {
        builder.ConfigureDbContextOptions(options => options.ConfigureOptionBuilder = configureOptions);
        builder.Services.AddDbContext<TContext>();
        return builder;
    }

    /// <summary>
    /// 添加 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 服务。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContext(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions)
        => builder.AddDbContext<BizerDbContext>(configureOptions);


    /// <summary>
    /// 添加继承 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 工厂服务。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContextFactory<TContext>(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions) where TContext : BizerDbContext
    {
        builder.ConfigureDbContextOptions(options => options.ConfigureOptionBuilder = configureOptions);
        builder.Services.AddDbContextFactory<TContext>(configureOptions);
        return builder;
    }

    /// <summary>
    /// 添加 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 工厂服务。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContextFactory(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions)
        => builder.AddDbContextFactory<BizerDbContext>(configureOptions);


    /// <summary>
    /// 添加继承 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 服务并设置线程池。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContextPool<TContext>(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions,int poolSize=1024) where TContext : BizerDbContext
    {
        builder.ConfigureDbContextOptions(options => options.ConfigureOptionBuilder = configureOptions);
        builder.Services.AddDbContextPool<TContext>(configureOptions,poolSize);
        return builder;
    }


    /// <summary>
    /// 添加 <see cref="BizerDbContext"/> 的 <see cref="DbContext"/> 服务并设置线程池。
    /// <para>
    /// 使用该方法才可以使用 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 的基类。
    /// </para>
    /// </summary>
    /// <typeparam name="TContext">数据库上下文类型。</typeparam>
    /// <param name="builder"></param>
    /// <param name="configureOptions">配置类型。</param>
    /// <returns></returns>
    public static BizerBuilder AddDbContextPool(this BizerBuilder builder, Action<DbContextOptionsBuilder> configureOptions)
        => builder.AddDbContextFactory<BizerDbContext>(configureOptions);

    /// <summary>
    /// 配置 <see cref="DbContextConfigureOptions"/>。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure">用于配置的委托。</param>
    /// <returns></returns>
    public static BizerBuilder ConfigureDbContextOptions(this BizerBuilder builder, Action<DbContextConfigureOptions> configure)
    {
        var options = new DbContextConfigureOptions();
        configure.Invoke(options);

        builder.Services.AddSingleton(options);
        return builder;
    }
}
