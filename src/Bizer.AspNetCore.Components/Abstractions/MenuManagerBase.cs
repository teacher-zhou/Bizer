namespace Bizer.AspNetCore.Components.Abstractions;

/// <summary>
/// 菜单管理的基类。
/// </summary>
public abstract class MenuManagerBase : IMenuManager
{
    /// <inheritdoc/>
    public virtual Task<IEnumerable<MenuItem>> GetInfoMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();

    /// <inheritdoc/>
    public virtual Task<IEnumerable<MenuItem>> GetNavbarMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();

    /// <inheritdoc/>
    public virtual Task<IEnumerable<MenuItem>> GetSidebarMenusAsync() => Enumerable.Empty<MenuItem>().ToResultTask();
}
