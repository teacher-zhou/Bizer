namespace Bizer.Localization;

/// <summary>
/// 本地化资源的配置。
/// </summary>
internal class LocalizationOptions
{
    /// <summary>
    /// 初始化 <see cref="LocalizationOptions"/> 类的新实例。
    /// </summary>
    public LocalizationOptions()
    {
        ResourcePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "localizations");
    }

    /// <summary>
    /// 获取或设置本地化资源存储的路径。
    /// </summary>
    public string ResourcePath { get; set; }

    /// <summary>
    /// 获取或设置当前的本地化名称。例如 zh-cn, en-us。默认是 en-us。
    /// </summary>
    /// <value>要符合国际定义的本地化资源。</value>
    public string Culture { get; set; } = "en-us";

    internal string LocalizeFilePath => string.Concat(Path.Combine(ResourcePath, Culture), ".json");

}