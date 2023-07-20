using Mapster;

namespace Bizer.Services;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加映射的服务，后续可以使用 <see cref="IMapper"/> 对象操作映射。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddMapper(this BizerBuilder builder,Action<TypeAdapterConfig>? configure=default)
    {
        TypeAdapterConfig config = new();
        configure?.Invoke(config);
        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();
        return builder;
    }
}
