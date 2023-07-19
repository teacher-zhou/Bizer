using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Stores;
/// <summary>
/// 表示可存储的构造器。
/// </summary>
public class BizerStoreBuilder
{
    internal BizerStoreBuilder(BizerBuilder builder) => Builder = builder;

    /// <summary>
    /// <see cref="Builder"/> 
    /// </summary>
    protected BizerBuilder Builder { get; }

    /// <summary>
    /// 
    /// </summary>
    public IServiceCollection Services =>Builder.Services;

    /// <summary>
    /// 添加工作单元的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TUnitOfWork">工作单元的类型。</typeparam>
    public  BizerStoreBuilder AddUnitOfWork<TUnitOfWork>() where TUnitOfWork : class, IUnitOfWork
    {
        Services.AddScoped<IUnitOfWork, TUnitOfWork>();
        return this;
    }


    /// <summary>
    /// 添加指定仓储服务和仓储实现。
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TRepositoryService">仓储服务的类型。</typeparam>
    /// <typeparam name="TRepositoryImplementation">仓储实现的类型。</typeparam>
    public  BizerStoreBuilder AddRepository<TRepositoryService, TRepositoryImplementation>()
        where TRepositoryService : class
        where TRepositoryImplementation : class, TRepositoryService
    => AddRepository(typeof(TRepositoryService), typeof(TRepositoryImplementation));

    /// <summary>
    /// 添加对 <see cref="IRepository{ TEntity}"/> 的通用仓储实现的服务。
    /// <para>
    /// 在代码中可使用 <c>IRepository&lt;Entity></c> 的类型注入服务。
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
    /// </para>
    /// </summary>
    /// <param name="repositoryImplementationType">仓储服务实现的类型。</param>
    public  BizerStoreBuilder AddDefaultRepository( Type repositoryImplementationType)
    => AddRepository(typeof(IRepository<>), repositoryImplementationType);

    /// <summary>
    /// 添加指定仓储服务的类型和对应的仓储服务实现的类型。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="repositoryServiceType">仓储服务的类型。</param>
    /// <param name="repositoryImplementationType">仓储服务实现的类型。</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="repositoryServiceType"/> 是 <c>null</c> 或 <paramref name="repositoryImplementationType"/> 是 <c>null</c>。
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="repositoryImplementationType"/> 不是 <paramref name="repositoryServiceType"/> 的实现类。
    /// </exception>
    public  BizerStoreBuilder AddRepository( Type repositoryServiceType, Type repositoryImplementationType)
    {
        if ( repositoryServiceType is null )
        {
            throw new ArgumentNullException(nameof(repositoryServiceType));
        }

        if ( repositoryImplementationType is null )
        {
            throw new ArgumentNullException(nameof(repositoryImplementationType));
        }

        Services.AddScoped(repositoryServiceType, repositoryImplementationType);
        return this;
    }
}
