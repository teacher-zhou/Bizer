using Bizer;
using Bizer.AspNetCore;
using Bizer.AspNetCore.Conventions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

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
    /// 构造 Bizer 框架。
    /// </summary>
    public static WebApplication WithBizer(this WebApplication application)
    {
        App.Initialize(application.Services);
        return application;
    }
}