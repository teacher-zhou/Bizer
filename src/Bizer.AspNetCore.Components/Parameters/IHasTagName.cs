namespace Bizer.AspNetCore.Components.Parameters;

/// <summary>
/// 表示具备自定义的 HTML 名称的组件。
/// </summary>
internal interface IHasTagName
{
    /// <summary>
    /// HTML 元素名称。
    /// </summary>
    string TriggerTagName { get; set; }
}
