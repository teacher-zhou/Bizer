using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Bizer.Localization;

/// <summary>
/// 本地化的扩展。
/// </summary>
public static class LocalizationDependencyInjectionExtensions
{
    /// <summary>
    /// 添加使用 JSON 文件作为本地化服务。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="culture"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static BizerBuilder AddJsonLocalization(this BizerBuilder builder, string culture = "en-us", string path = "localizations")
    {
        LocalizationOptions options = new()
        {
            ResourcePath = path,
            Culture = culture,
        };
        builder.Services.AddSingleton(options);
        builder.Services.TryAddSingleton<IStringLocalizerFactory, JsonLocalizerStringFactory>();
        builder.Services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        return builder;
    }
}
