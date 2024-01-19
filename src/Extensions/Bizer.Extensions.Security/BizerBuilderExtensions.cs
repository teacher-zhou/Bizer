using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bizer.Extensions.Security;

public static class BizerBuilderExtensions
{

    /// <summary>
    /// 添加当前线程作为主体的访问器。
    /// </summary>
    /// <param name="builder"></param>
    static BizerBuilder AddThreadCurrentPrincipalAccessor(this BizerBuilder builder)
        => builder.AddCurrentPrincipalAccessor<ThreadCurrentPrincipalAccessor>();

    /// <summary>
    /// 添加主体访问器的服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TCurrentPrincipalAccessor">主体访问器类型。</typeparam>
    public static BizerBuilder AddCurrentPrincipalAccessor<TCurrentPrincipalAccessor>(this BizerBuilder builder)
        where TCurrentPrincipalAccessor : class, ICurrentPrincipalAccessor
    {
        builder.Services.TryAddTransient<ICurrentPrincipalAccessor, TCurrentPrincipalAccessor>();
        return builder;
    }
}
