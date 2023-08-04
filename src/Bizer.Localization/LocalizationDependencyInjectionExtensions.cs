using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Bizer.Localization;

/// <summary>
/// 本地化的扩展。
/// </summary>
public static class LocalizationDependencyInjectionExtensions
{
    /// <summary>
    /// 添加使用 JSON 文件作为本地化服务。资源文件必须是 .json 的文件
    /// <para>
    /// JSON 文件格式：
    /// <code language="javascript">
    /// {
    ///     "Culture" : "zh-cn",
    ///     "Values":{
    ///         "Key1" : "翻译1...",
    ///         "Key2" : "翻译2..."
    ///     }
    /// }
    /// </code>
    /// </para>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddJsonLocalization(this BizerBuilder builder, Action<LocalizationOptions>? configure=default)
    {
        LocalizationOptions options = new();
        configure?.Invoke(options);

        builder.Services.AddSingleton(options);
        builder.Services.TryAddSingleton<IStringLocalizerFactory, JsonLocalizerStringFactory>();
        builder.Services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        return builder;
    }
}
