using Bizer;
using Bizer.AspNetCore;
using Bizer.AspNetCore.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerAspNetCoreDependencyInjections
{
    /// <summary>
    /// 添加秒变API的转换功能。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure">一个用于配置的委托。</param>
    /// <returns></returns>
    public static BizerBuilder AddApiConvension(this BizerBuilder builder, Action<BizerApiOptions>? configure = default)
    {
        BizerApiOptions apiOptions = new();
        configure?.Invoke(apiOptions);

        builder.Services.AddSingleton(apiOptions);

        builder.Services.AddSwaggerDocument(apiOptions.ConfigureSwaggerDocument);
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new DynamicHttpApiConvention(apiOptions, new DefaultHttpRemotingResolver()));
            options.Filters.Add(new ProducesAttribute("application/json"));
        })
        .ConfigureApplicationPartManager(applicationPart =>
        {
            foreach ( var assembly in builder.AutoDiscovery.GetDiscoveredAssemblies() )
            {
                applicationPart.ApplicationParts.Add(new AssemblyPart(assembly));
            }
            applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
        });

        return builder;
    }

    /// <summary>
    /// 使用 Bizer OpenAPI 带 Swagger 的中间件。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseBizerOpenApi(this IApplicationBuilder builder)
    {
        var env = builder.ApplicationServices.GetRequiredService<IHostEnvironment>();

        if ( env.IsDevelopment() )
        {
            builder.UseSwaggerUi3().UseOpenApi();
        }

        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        return builder;
    }
}