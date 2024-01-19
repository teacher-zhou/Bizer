using Bizer;
using Bizer.AspNetCore;

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
}