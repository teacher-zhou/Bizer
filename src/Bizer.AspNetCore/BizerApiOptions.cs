using NSwag.Generation;

namespace Bizer.AspNetCore;

/// <summary>
/// AspNetCore 的配置。
/// </summary>
public class BizerApiOptions:AutoDiscoveryOptions
{
    /// <summary>
    /// 初始化 <see cref="ServerOptions"/> 类的新实例。
    /// </summary>
    public BizerApiOptions()
    {
        ConfigureSwaggerDocument ??= setting => setting.Title = "API 文档";
    }

    /// <summary>
    /// 配置 Swagger 文档。
    /// </summary>
    public Action<OpenApiDocumentGeneratorSettings> ConfigureSwaggerDocument { get; set; }
    /// <summary>
    /// 控制器后缀名称的过滤关键字。
    /// </summary>
    public IList<string> ControllerPrefixKeywords { get; } = new List<string>()
    {
        "BusinessService", "AppService", "ApplicationService", "BizService", "Service"
    };
    /// <summary>
    /// Action 前缀的过滤关键字。
    /// </summary>
    public IList<string> ActionSuffixKeywords { get; } = new List<string>()
    {
        "Get", "Post", "Create", "Add", "Insert", "Put", "Update", "Patch", "Delete", "Remove"
    };
    /// <summary>
    /// 根据前缀自动匹配的 <see cref="HttpMethod"/> 对象。
    /// </summary>
    public IDictionary<string, HttpMethod> HttpMethodSuffixMapping { get; } = new Dictionary<string, HttpMethod>()
    {
        ["Create"] = HttpMethod.Post,
        ["Add"] = HttpMethod.Post,
        ["Insert"] = HttpMethod.Post,
        ["Update"] = HttpMethod.Put,
        ["Edit"] = HttpMethod.Put,
        ["Patch"] = HttpMethod.Put,
        ["Delete"] = HttpMethod.Delete,
        ["Remove"] = HttpMethod.Delete,
        ["Get"] = HttpMethod.Get,
        ["Find"] = HttpMethod.Get,
    };
}
