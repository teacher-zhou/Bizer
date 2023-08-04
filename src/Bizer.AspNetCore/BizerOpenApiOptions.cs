using NSwag.Generation;

namespace Bizer.AspNetCore;

/// <summary>
/// AspNetCore 的配置。
/// </summary>
public class BizerOpenApiOptions
{
    /// <summary>
    /// 初始化 <see cref="ServerOptions"/> 类的新实例。
    /// </summary>
    public BizerOpenApiOptions()
    {
        ConfigureSwaggerDocument ??= setting =>
        {
            setting.Title = Title;
            setting.Description = Description;
            setting.Version = Version;
            setting.UseControllerSummaryAsTagDescription = true;
        };
    }

    /// <summary>
    /// 获取或设置 Swagger 文档的标题。
    /// </summary>
    public string? Title { get; set; } = "API 文档";
    /// <summary>
    /// 获取或设置 Swagger 文档的描述。
    /// </summary>
    public string? Description { get; set; } = "这里是对外暴露的 API 文档";
    /// <summary>
    /// 获取或设置 Swagger 文档的版本号。
    /// </summary>
    public string? Version { get; set; } = "1.0";

    /// <summary>
    /// 配置 Swagger 文档。
    /// </summary>
    public Action<OpenApiDocumentGeneratorSettings> ConfigureSwaggerDocument { get; set; }
}

