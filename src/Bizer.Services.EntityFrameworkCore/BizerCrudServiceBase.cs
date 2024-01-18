namespace Bizer.Services.EntityFrameworkCore;
/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity> : BizerCrudServiceBase<TKey, TEntity, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class
{

    /// <summary>
    /// 初始化 <see cref="BizerCrudServiceBase{TKey, TEntity}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TModel">CURD字段的模型类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity, TModel> : BizerCrudServiceBase<TKey, TEntity, TModel, TModel>
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TModel : class
{

    /// <summary>
    /// 初始化 <see cref="CrudServiceBase{TKey, TEntity, TModel}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplay">详情、列表和过滤字段的模型类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TDisplay> : BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TDisplay, TDisplay>
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDisplay : class
{

    /// <summary>
    /// 初始化 <see cref="BizerCrudServiceBase{ TKey, TEntity, TCreateOrUpdate, TDisplay}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDisplay">详情和列表字段的模型类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TDisplay, TListFilter> : BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TCreateOrUpdate, TDisplay, TDisplay, TListFilter>
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDisplay : class
    where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="BizerCrudServiceBase{TKey, TEntity, TCreateOrUpdate, TDisplay, TListFilter}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TContext">数据库上下文类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreateOrUpdate">创建或更新字段的模型类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TDetail, TList, TListFilter> : BizerCrudServiceBase<TKey, TEntity, TCreateOrUpdate, TCreateOrUpdate, TDetail, TList, TListFilter>
    where TKey : IEquatable<TKey>
    where TEntity : class
    where TCreateOrUpdate : class
    where TDetail : class
    where TList : class
    where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="BizerCrudServiceBase{ TKey, TEntity, TCreateOrUpdate, TDetail, TList, TListFilter}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// 使用 <see cref="BizerDbContext"/> 来操作 CRUD 服务的基类。
/// <para>
/// 使用这种方式，需要注册 <see cref="DependencyInjectionExtensions.AddDbContext(BizerBuilder, Action{DbContextOptionsBuilder})"/> 服务。
/// </para>
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TEntity">实体类型。</typeparam>
/// <typeparam name="TCreate">创建字段的模型类型。</typeparam>
/// <typeparam name="TUpdate">更新字段的模型类型。</typeparam>
/// <typeparam name="TDetail">详情字段的模型类型。</typeparam>
/// <typeparam name="TList">列表字段的类型。</typeparam>
/// <typeparam name="TListFilter">列表过滤的模型类型。</typeparam>
public abstract class BizerCrudServiceBase<TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter> : CrudServiceBase<BizerDbContext, TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter>
where TKey : IEquatable<TKey>
where TEntity : class
where TCreate : class
where TUpdate : class
where TDetail : class
where TList : class
where TListFilter : class
{

    /// <summary>
    /// 初始化 <see cref="BizerCrudServiceBase{TKey, TEntity, TCreate, TUpdate, TDetail, TList, TListFilter}"/> 类的新实例。
    /// </summary>
    protected BizerCrudServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}