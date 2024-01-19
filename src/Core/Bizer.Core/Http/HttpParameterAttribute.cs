namespace Bizer;

/// <summary>
/// 定义参数的访问方式。
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public abstract class HttpParameterAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="HttpParameterAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="type">参数访问类型。</param>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public HttpParameterAttribute(HttpParameterType type = HttpParameterType.FromPath, string? name = default)
    {
        Type = type;
        Name = name;
    }
    /// <summary>
    /// 获取参数访问类型。
    /// </summary>
    public HttpParameterType Type { get; }
    /// <summary>
    /// 获取参数名称。
    /// </summary>
    public string? Name { get; }
}

/// <summary>
/// HTTP 参数的类型。
/// </summary>
public enum HttpParameterType
{
    /// <summary>
    /// 从路由获取参数。这是默认的。
    /// </summary>
    FromPath = 0,
    /// <summary>
    /// 从 Body 中获取参数的值。
    /// </summary>
    FromBody,
    /// <summary>
    /// 从查询字符串中获取参数的值。
    /// </summary>
    FromQuery,
    /// <summary>
    /// 从 Header 中获取参数的值。
    /// </summary>
    FromHeader,
    /// <summary>
    /// 从 form 中获取参数的值。
    /// </summary>
    FromForm,
}

/// <summary>
/// 定义请求参数从查询语句中获取。
/// </summary>
public class QueryAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="QueryAttribute"/> 类的新实例。
    /// </summary>
    public QueryAttribute() : this(default) { }
    /// <summary>
    /// 初始化 <see cref="QueryAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public QueryAttribute(string? name) : base(HttpParameterType.FromQuery, name)
    {

    }
}

/// <summary>
/// 参数将从 Header 中获取值。
/// </summary>
public class HeaderAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="HeaderAttribute"/> 类的新实例。
    /// </summary>
    public HeaderAttribute() : this(default) { }
    /// <summary>
    /// 初始化 <see cref="HeaderAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public HeaderAttribute(string? name) : base(HttpParameterType.FromHeader, name)
    {

    }
}

/// <summary>
/// 定义请求参数从 form 中获取。
/// </summary>
public class FormAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="FormAttribute"/> 类的新实例。
    /// </summary>
    public FormAttribute() : this(default) { }
    /// <summary>
    /// 初始化 <see cref="FormAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public FormAttribute(string? name) : base(HttpParameterType.FromForm, name)
    {

    }
}
/// <summary>
/// 要求参数必须是使用 Body 的形式访问。
/// </summary>
public class BodyAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="BodyAttribute"/> 类的新实例。
    /// </summary>
    public BodyAttribute() : this(default) { }
    /// <summary>
    /// 初始化 <see cref="BodyAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public BodyAttribute(string? name) : base(HttpParameterType.FromBody, name)
    {

    }
}
/// <summary>
/// 请求参数可以用 api 路径的形式访问。
/// </summary>
public class PathAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="PathAttribute"/> 类的新实例。
    /// </summary>
    public PathAttribute() : this(default) { }

    /// <summary>
    /// 初始化 <see cref="PathAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public PathAttribute(string? name) : base(HttpParameterType.FromPath, name) { }
}
