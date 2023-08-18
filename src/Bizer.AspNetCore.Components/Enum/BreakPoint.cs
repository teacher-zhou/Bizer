namespace Bizer.AspNetCore.Components;

/// <summary>
/// 响应式断点。
/// </summary>
public enum BreakPoint
{
    /// <summary>
    /// 超小，< 576px
    /// </summary>
    [CssClass] XS = 0,
    /// <summary>
    /// 小，>= 576px
    /// </summary>
    [CssClass("sm")] SM = 1,
    /// <summary>
    /// 中等，>=768
    /// </summary>
    [CssClass("md")] MD,
    /// <summary>
    /// 大型，>=992px
    /// </summary>
    [CssClass("lg")] LG,
    /// <summary>
    /// 超大，>=1200px
    /// </summary>
    [CssClass("xl")] XL,
    /// <summary>
    /// 超级大，>=1400
    /// </summary>
    [CssClass("xxl")] XXL
}
