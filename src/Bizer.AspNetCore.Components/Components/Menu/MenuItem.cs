namespace Bizer.AspNetCore.Components;

/// <summary>
/// 导航菜单信息。
/// </summary>
public class MenuItem
{
    /// <summary>
    /// 初始化 <see cref="MenuItem"/> 类的新实例。
    /// </summary>
    public MenuItem() { }
    /// <summary>
    /// 初始化 <see cref="MenuItem"/> 类的新实例。
    /// </summary>
    /// <param name="title">菜单标题。</param>
    /// <param name="link">链接。</param>
    /// <param name="icon">图标。</param>
    public MenuItem(string? title,string? link=default, object? icon=default)
    {
        Title = title;
        Link = link;
        Icon = icon;
    }

    /// <summary>
    /// 显示的标题。
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// 图标。
    /// </summary>
    public object? Icon { get; set; }
    /// <summary>
    /// 跳转的链接。
    /// </summary>
    public string? Link { get; set; }
    /// <summary>
    /// 作为下拉菜单时的分割线。仅在二级菜单有效。
    /// </summary>
    public bool Divider { get; set; }
    /// <summary>
    /// 禁用状态。
    /// </summary>
    public bool Disabled { get; set; }
    /// <summary>
    /// 子菜单集合。
    /// </summary>
    public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

}
