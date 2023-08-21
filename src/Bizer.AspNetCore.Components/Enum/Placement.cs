namespace Bizer.AspNetCore.Components;
public enum Position
{
    Fixed,
    Relative,
    Sticky,
    Absolut
}

/// <summary>
/// 放置的位置。
/// </summary>
public enum Placement
{
    /// <summary>
    /// 没有任何位置。
    /// </summary>
    None,
    /// <summary>
    /// 顶部居左。
    /// </summary>
    [CssClass("top-0 start-0")] TopLeft,
    /// <summary>
    /// 顶部居中。
    /// </summary>
    [CssClass("top-0 start-50 translate-middle-x")] TopCenter,
    /// <summary>
    /// 顶部居右。
    /// </summary>
    [CssClass("top-0 end-0")] TopRight,
    /// <summary>
    /// 底部居左。
    /// </summary>
    [CssClass("bottom-0 start-0")] BottomLeft,
    /// <summary>
    /// 底部居中。
    /// </summary>
    [CssClass("bottom-0 start-50 translate-middle-x")] BottomCenter,
    /// <summary>
    /// 底部居右。
    /// </summary>
    [CssClass("bottom-0 end-0")] BottomRight,
    /// <summary>
    /// 中部居左。
    /// </summary>
    [CssClass("top-50 start-0 translate-middle-y")] MiddleLeft,
    /// <summary>
    /// 中部居中。
    /// </summary>
    [CssClass("top-50 start-50 translate-middle")] MiddleCenter,
    /// <summary>
    /// 中部居右。
    /// </summary>
    [CssClass("top-50 end-50 translate-middle-y")] MiddleRight,
}