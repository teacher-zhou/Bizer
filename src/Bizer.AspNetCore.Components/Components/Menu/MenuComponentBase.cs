using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public abstract class MenuComponentBase : ComponentBase
{
    [Inject]protected BizerComponentOptions Options { get; set; }
    [Inject] IMenuManager MenuManager { get; set; }

    protected ThemeContrast Theme => Options.MenuBackground.IsDark() ? ThemeContrast.Dark : ThemeContrast.Light;
    protected IEnumerable<MenuItem> MenuItems { get; set; } = Enumerable.Empty<MenuItem>();
    protected IEnumerable<MenuItem> InfoItems { get; set; } = Enumerable.Empty<MenuItem>();


    /// <summary>
    /// 是否显示品牌。
    /// </summary>
    [Parameter] public bool BrandVisibile { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            MenuItems = await MenuManager.GetNavbarMenusAsync();
            InfoItems = await MenuManager.GetInfoMenusAsync();
        }
    }
}
