using Bizer;
using Bizer.AspNetCore.Components;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加组件的相关服务和配置。
    /// </summary>
    public static BizerBuilder AddComponents(this BizerBuilder builder, Action<BizerComponentOptions>? configure = default)
    {
        var options = new BizerComponentOptions();
        configure?.Invoke(options);
        builder.Services.AddSingleton(options);

        builder.Services.AddBlazorise().AddBootstrap5Components().AddBootstrap5Providers().AddFontAwesomeIcons();

        return builder;
    }
}
