namespace Bizer.Localization;

/// <summary>
/// 本地化资源的配置。
/// </summary>
public class LocalizationOptions
{
    /// <summary>
    /// 初始化 <see cref="LocalizationOptions"/> 类的新实例。
    /// </summary>
    public LocalizationOptions()
    {
        ResourcePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "localizations");
        LocaleFileName = Culture;
    }

    /// <summary>
    /// 获取或设置本地化资源存储的路径。
    /// </summary>
    /// <value>默认值是 localizations。</value>
    public string ResourcePath { get; set; } = "localizations";

    /// <summary>
    /// 获取或设置合国际定义的本地化资源名称。
    /// </summary>
    /// <value>例如 zh-cn, en-us。默认是 en-us。</value>
    public string Culture { get; set; } = "en-us";

    /// <summary>
    /// 资源包文件名称。
    /// </summary>
    /// <value>默认值与 <see cref="Culture"/> 一致。</value>
    public string LocaleFileName { get; set; }

    internal string LocalizeFilePath => string.Concat(Path.Combine(ResourcePath, LocaleFileName), ".json");

}