using System.Reflection;

namespace Bizer.AspNetCore.Components;

partial class App
{
    [Inject] AutoDiscoveryOptions AutoDiscoveryOptions { get; set; }
    [Inject] BizerComponentOptions ComponentOptions { get; set; }

    IEnumerable<Assembly> GetBlazorAssemblies()
    {
        return AutoDiscoveryOptions.GetDiscoveredAssemblies().Where(ass => ComponentOptions.Blazor.AppType.Assembly != ass);
    }

    RenderFragment GetFoundView(RouteData data) 
        => builder => builder.CreateComponent(ComponentOptions.Blazor.FoundViewType, 0, attributes: new
    {
        Data = data
    });
    RenderFragment GetNotFoundView() 
        => builder => builder.CreateComponent(ComponentOptions.Blazor.NotFoundViewType, 0);
}
