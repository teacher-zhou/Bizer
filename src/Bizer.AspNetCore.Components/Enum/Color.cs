namespace Bizer.AspNetCore.Components;
/// <summary>
/// 通用颜色。
/// </summary>
public enum Color
{
    [CssClass] None,
    Primary,
    Secondary,
    Info,
    Success,
    Warning,
    Danger,
    Light,
    Dark
}
/// <summary>
/// 背景颜色。
/// </summary>
[CssClass("bg-")]
public enum BgColor
{
    Primary,
    Secondary,
    Info,
    Success,
    Warning,
    Danger,
    Light,
    Dark,
    Body,
    White,
    Transparnet
}
/// <summary>
/// 文本颜色。
/// </summary>
[CssClass("text-")]
public enum TextColor
{
    Primary,
    Secondary,
    Info,
    Success,
    Warning,
    Danger,
    Light,
    Dark,
    Body,
    Muted,
    White,
    [CssClass("black-50")] Black50,
    [CssClass("white-50")] White50,
}