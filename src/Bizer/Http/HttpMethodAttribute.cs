namespace Bizer.Http;

/// <summary>
/// 定义 HTTP 的请求方式的特性。
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class HttpMethodAttribute : Attribute, IRouteProvider
{
    /// <summary>
    /// 初始化 <see cref="HttpMethodAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="method">请求方式。</param>
    /// <param name="template">路由模板。</param>
    public HttpMethodAttribute(HttpMethod method, string? template = default)
    {
        Template = template;
        Method = method;
    }
    /// <summary>
    /// 获取路由模板。
    /// </summary>
    public string? Template { get; }
    /// <summary>
    /// 获取请求方式。
    /// </summary>
    public HttpMethod Method { get; }

    /// <summary>
    /// 获取或设置控制器的名称。
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Order { get; set; } = 100;
}

/// <summary>
/// 定义具备 DELETE 行为的 HTTP 请求方式。
/// </summary>
public class DeleteAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="DeleteAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public DeleteAttribute(string? template = default) : base(HttpMethod.Delete, template)
    {
    }
}
/// <summary>
/// 定义具备 GET 行为的 HTTP 请求方式。
/// </summary>
public class GetAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="GetAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public GetAttribute(string? template = default) : base(HttpMethod.Get, template)
    {
    }
}
/// <summary>
/// 定义具备 OPTIONS 行为的 HTTP 请求方式。
/// </summary>
public class OptionsAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="OptionsAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public OptionsAttribute(string? template = null) : base(HttpMethod.Options, template)
    {
    }
}
/// <summary>
/// 定义具备 PATCH 行为的 HTTP 请求方式。
/// </summary>
public class PatchAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PatchAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PatchAttribute(string? template = null) : base(HttpMethod.Patch, template)
    {
    }
}
/// <summary>
/// 定义具备 POST 行为的 HTTP 请求方式。
/// </summary>
public class PostAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PostAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PostAttribute(string? template = default) : base(HttpMethod.Post, template)
    {
    }
}
/// <summary>
/// 定义具备 PUT 行为的 HTTP 请求方式。
/// </summary>
public class PutAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="PutAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public PutAttribute(string? template = default) : base(HttpMethod.Put, template)
    {
    }
}
/// <summary>
/// 定义具备 TRACE 行为的 HTTP 请求方式。
/// </summary>
public class TraceAttribute : HttpMethodAttribute
{
    /// <summary>
    /// 初始化 <see cref="TraceAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public TraceAttribute(string? template = null) : base(HttpMethod.Trace, template)
    {
    }
}
