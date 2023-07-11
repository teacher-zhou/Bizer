using Bizer.Http;
using System.Diagnostics.CodeAnalysis;

namespace Bizer;

/// <summary>
/// 定义让接口具备 HTTP 方式访问的路由形式。标记该特性的接口可以被自动识别远程 api。
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class ApiRouteAttribute : Attribute, IRouteProvider
{
    /// <summary>
    /// 初始化 <see cref="ApiRouteAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public ApiRouteAttribute([NotNull] string template)
        => Template = template ?? throw new ArgumentNullException(nameof(template));
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Template { get; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Order { get; set; } = 1000;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Name { get; set; }
}
