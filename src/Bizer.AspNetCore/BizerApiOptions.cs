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
}
