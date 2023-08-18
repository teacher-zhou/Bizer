using Bizer.AspNetCore.Components.Layouts;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 组件库的配置。
/// </summary>
public class BizerComponentOptions
{
    /// <summary>
    /// 应用程序的标题。
    /// </summary>
    public string? AppName { get; set; } = "Bizer";
    /// <summary>
    /// 应用程序的 logo 地址。
    /// </summary>
    public string? AppLogoUrl { get; set; }

    /// <summary>
    /// 应用程序的根地址。
    /// </summary>
    public string? AppBaseAddress { get; set; } = "/";

    public Background MenuBackground { get; set; } = Background.Dark;

    public Theme ThemeConfiguration { get; set; } = new();

    public BlazorConfiguration Blazor { get; set; } = new();

    public class BlazorConfiguration
    {
        public Type AppType { get; set; } = typeof(App);

        public Type MainLayoutType { get; set; } = typeof(DefaultLayout);

        public Type FoundViewType { get; set; } = typeof(FoundView);
        public Type NotFoundViewType { get; set; } = typeof(NotFoundView);
    }
}
