namespace Bizer.AspNetCore.Components.Abstractions;

/// <summary>
/// 提供菜单导航的管理。
/// </summary>
public interface IMenuManager
{
    /// <summary>
    /// 以异步的方式获取顶部导航菜单。
    /// </summary>
    /// <returns>一个异步任务，当任务完成后返回可迭代的菜单项。</returns>
    Task<IEnumerable<MenuItem>> GetNavbarMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();
    /// <summary>
    /// 以异步的方式获取侧边栏菜单。
    /// </summary>
    /// <returns>一个异步任务，当任务完成后返回可迭代的菜单项。</returns>
    Task<IEnumerable<MenuItem>> GetSidebarMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();
    /// <summary>
    /// 以异步的方式获取其他各种信息的菜单。
    /// </summary>
    /// <returns>一个异步任务，当任务完成后返回可迭代的菜单项。</returns>
    Task<IEnumerable<MenuItem>> GetInfoMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();

}
