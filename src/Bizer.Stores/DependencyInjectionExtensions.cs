namespace Bizer.Stores;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加存储构造器。
    /// </summary>
    /// <param name="configure">存储的配置。</param>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddStores(this BizerBuilder builder, Action<BizerStoreBuilder> configure)
    {
        configure(new(builder));
        return builder;
    }
}
