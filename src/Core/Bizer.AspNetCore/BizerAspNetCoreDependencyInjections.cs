using Bizer;
using Bizer.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer;
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
    /// 允许任意方式的跨域请求。适用于 localhost 的来源调试使用。
    /// <para>
    /// 注意：该方法请在开发环境调试时使用。
    /// </para>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseAnyCors(this IApplicationBuilder app)
        => app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}