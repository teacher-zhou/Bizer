using Bizer;
using Bizer.AspNetCore;
using Bizer.AspNetCore.Conventions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Hosting;

using NSwag.Generation;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerAspNetCoreDependencyInjections
{
    /// <summary>
    /// 添加动态 API 的功能。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddDynamicWebApi(this BizerBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new DynamicHttpApiConvention(new DefaultHttpRemotingResolver()));
            options.Filters.Add(new ProducesAttribute("application/json"));
        })
        .ConfigureApplicationPartManager(applicationPart =>
        {
            foreach (var assembly in builder.AutoDiscovery.GetDiscoveredAssemblies())
            {
                applicationPart.ApplicationParts.Add(new AssemblyPart(assembly));
            }
            applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
        });

        return builder;
    }

    /// <summary>
    /// 添加动态 API 的功能并配置 Swagger 的设置。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddDynamicWebApi(this BizerBuilder builder, Action<OpenApiDocumentGeneratorSettings> configure)
        => builder.AddDynamicWebApi().AddSwagger(configure);

    /// <summary>
    /// 添加 Swagger 的功能并进行配置。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static BizerBuilder AddSwagger(this BizerBuilder builder, Action<OpenApiDocumentGeneratorSettings> configure)
    {
        builder.Services.AddSwaggerDocument(configure);
        return builder;
    }

    /// <summary>
    /// 添加从 HttpContext 解析的主体访问器。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static BizerBuilder AddHttpContextPricipalAccessor(this BizerBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        return builder.AddCurrentPrincipalAccessor<HttpContextPrincipalAccessor>();
    }

    /// <summary>
    /// 使用 Bizer OpenAPI 带 Swagger 的中间件。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseBizer(this IApplicationBuilder builder, Action<BizerMiddlewareOptions>? configure = default)
    {
        var env = builder.ApplicationServices.GetRequiredService<IHostEnvironment>();

        var options = new BizerMiddlewareOptions(env);
        configure?.Invoke(options);

        if (!builder.ApplicationServices.TryGetService<OpenApiDocumentGeneratorSettings>(out var apiOptions))
        {
            throw new InvalidOperationException($"需要先添加 Bizer 的 '{nameof(AddDynamicWebApi)}' 的服务才可以使用 '{nameof(UseBizer)}' 中间件");
        }


        if (options!.EnableSwaggerPath)
        {
            builder
                .UseSwaggerUi(setting =>
                {
                    setting.DocumentTitle = apiOptions?.Title;
                    setting.Path = "/swagger";
                });
        }

        if (options!.EnableRedocPath)
        {
            builder
                .UseReDoc(setting =>
                {
                    setting.DocumentTitle = apiOptions?.Title;
                    setting.Path = "/redoc";
                });
        }

        builder.UseOpenApi(setting =>
                {
                    setting.DocumentName = apiOptions?.Version;
                });

        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


        return builder;
    }

    /// <summary>
    /// 构造 Bizer 框架。
    /// </summary>
    public static WebApplication WithBizer(this WebApplication application)
    {
        App.Initialize(application.Services);
        return application;
    }
}