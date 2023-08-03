namespace Bizer;

/// <summary>
/// 继承以实现模块化配置。
/// </summary>
public abstract class AppModule
{
    /// <summary>
    /// 配置注入。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> 实例。</param>
    public virtual void ConfigureServices(IServiceCollection services) { }

    /// <summary>
    /// 配置 Bizer 框架。
    /// </summary>
    /// <param name="builder"><see cref="BizerBuilder"/> 实例。</param>
    public virtual void ConfigureBizer(BizerBuilder builder) { }
}
