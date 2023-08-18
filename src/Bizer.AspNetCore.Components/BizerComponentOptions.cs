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
    /// 获取或设置全局主题配色。
    /// </summary>
    public ThemeOptions Theme { get; set; } = new();

    public BlazorConfiguration Blazor { get; set; } = new();
}


public class BlazorConfiguration
{
    public Type AppType { get; set; } = typeof(App);

    public Type MainLayoutType { get; set; } = typeof(DefaultLayout);

    public Type FoundViewType { get; set; } = typeof(FoundView);
    public Type NotFoundViewType { get; set; } = typeof(NotFoundView);
}


/// <summary>
/// 全局主题配色配置。
/// </summary>
public class ThemeOptions
{
    public ThemeOptions()
    {
    }

    /// <summary>
    /// 导航菜单的颜色。
    /// </summary>
    public Color MenuColor { get; set; } = Color.Dark;
}