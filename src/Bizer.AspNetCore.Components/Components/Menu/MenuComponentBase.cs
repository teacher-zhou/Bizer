using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public abstract class MenuComponentBase : ComponentBase
{
    [Inject]protected BizerComponentOptions Options { get; set; }
    [Inject] IMenuManager MenuManager { get; set; }

    /// <summary>
    /// 获取主题的对比色。
    /// </summary>
    protected ThemeContrast ThemeContrast => Options.MenuBackground.IsDark() ? ThemeContrast.Dark : ThemeContrast.Light;
    /// <summary>
    /// 获取导航菜单。
    /// </summary>
    protected IEnumerable<MenuItem> NavbarMenus { get;private set; } = Enumerable.Empty<MenuItem>();
    /// <summary>
    /// 获取信息菜单。
    /// </summary>
    protected IEnumerable<MenuItem> InfoMenus { get;private set; } = Enumerable.Empty<MenuItem>();
    /// <summary>
    /// 获取侧边菜单。
    /// </summary>
    protected IEnumerable<MenuItem> SideMenus { get;private set; } = Enumerable.Empty<MenuItem>();


    /// <summary>
    /// 是否显示品牌。
    /// </summary>
    [Parameter] public bool BrandVisibile { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            NavbarMenus = await MenuManager.GetNavbarMenusAsync();
            InfoMenus = await MenuManager.GetInfoMenusAsync();
            SideMenus = await MenuManager.GetSidebarMenusAsync();
            StateHasChanged();
        }
    }
}
