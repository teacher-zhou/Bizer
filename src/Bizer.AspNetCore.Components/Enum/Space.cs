namespace Bizer.AspNetCore.Components;

/// <summary>
/// 间隙
/// </summary>
[CssClass("gap-")]
public enum Gap
{
    [CssClass("0")] Is0,
    [CssClass("1")] Is1,
    [CssClass("2")] Is2,
    [CssClass("3")] Is3,
    [CssClass("4")] Is4,
    [CssClass("5")] Is5,
}

public enum Space
{
    [CssClass("0")] None,
    [CssClass("1")] Is1,
    [CssClass("2")] Is2,
    [CssClass("3")] Is3,
    [CssClass("4")] Is4,
    [CssClass("5")] Is5,
}

/// <summary>
/// 行平均的列数量。
/// </summary>
[CssClass("row-cols-")]
public enum RowColumns
{
    Auto,
    [CssClass("1")] Is1,
    [CssClass("2")] Is2,
    [CssClass("3")] Is3,
    [CssClass("4")] Is4,
    [CssClass("5")] Is5,
    [CssClass("6")] Is6,
}

public enum Columns
{
    Auto,
    [CssClass("1")] Is1,
    [CssClass("2")] Is2,
    [CssClass("3")] Is3,
    [CssClass("4")] Is4,
    [CssClass("5")] Is5,
    [CssClass("6")] Is6,
    [CssClass("7")] Is7,
    [CssClass("8")] Is8,
    [CssClass("9")] Is9,
    [CssClass("10")] Is10,
    [CssClass("11")] Is11,
    [CssClass("12")] Is12,
}