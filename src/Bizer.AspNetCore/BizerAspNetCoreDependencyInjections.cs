using Bizer.AspNetCore;
using Bizer.AspNetCore.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;
public static class BizerAspNetCoreDependencyInjections
{
    public static IServiceCollection AddBizerOpenApi(this IServiceCollection services,Action<BizerApiOptions>? configure=default)
    {
        BizerApiOptions options = new();
        configure?.Invoke(options);

        services.AddSwaggerDocument(options.ConfigureSwaggerDocument);
        services.AddEndpointsApiExplorer();
        services.AddControllers(options =>
        {
            options.Conventions.Add(new DynamicHttpApiConvention());
            options.Filters.Add(new ProducesAttribute("application/json"));
        })
        .ConfigureApplicationPartManager(applicationPart =>
        {
            foreach ( var assembly in options.GetMatchesAssemblies() )
            {
                applicationPart.ApplicationParts.Add(new AssemblyPart(assembly));
            }
            applicationPart.FeatureProviders.Add(new DynamicHttpApiControllerFeatureProvider());
        });
        return services;
    }

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