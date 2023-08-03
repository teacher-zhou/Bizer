namespace Bizer.Localization;


/// <summary>
/// 本地化资源的数据结构。
/// </summary>
internal class LocalizerStructure
{
    /// <summary>
    /// 获取或设置本地化资源的名称。
    /// </summary>
    public string Culture { get; set; }
    /// <summary>
    /// 本地化资源包含的语言包字典。
    /// </summary>
    public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
}
