using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components.Layouts;

partial class DefaultLayout
{
    /// <summary>
    /// 菜单管理器。
    /// </summary>
    [Inject]IMenuManager MenuManager { get; set; }
}
