using Bizer;
using Bizer.AspNetCore.Components;

using ComponentBuilder.Interceptors;
using ComponentBuilder.Resolvers;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 添加组件的相关服务和配置。
    /// </summary>
    public static BizerComponentBuilder AddComponents(this BizerBuilder builder, Action<BizerComponentOptions>? configure = default)
    {
        var options = new BizerComponentOptions();
        configure?.Invoke(options);
        builder.Services.AddSingleton(options);

        builder.Services.AddComponentBuilder(conf=>conf.AddDefaultConfigurations().AddFluentClassResolver().AddConsoleDiagnostic());

        builder.Services.AddScoped<IToastService, ToastService>();

        var componentBuilder = new BizerComponentBuilder(builder);

        componentBuilder.AddDefaultMenuManager();



        return componentBuilder;
    }
}
