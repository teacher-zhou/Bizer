namespace Bizer.Http;

/// <summary>
/// 定义路由提供器。
/// </summary>
public interface IRouteProvider
{
    /// <summary>
    /// 获取路由模板。
    /// </summary>
    string? Template { get; }
    /// <summary>
    /// 获取或设置名称。
    /// </summary>
    string? Name { get; set; }
    /// <summary>
    /// 获取或设置路由匹配的顺序，从小到大进行匹配。
    /// </summary>
    int Order { get; set; }
}
