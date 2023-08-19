﻿namespace Bizer.AspNetCore.Components;

/// <summary>
/// 按钮尺寸。
/// </summary>
[CssClass("btn-")]
public enum ButtonSize
{
    [CssClass("sm")] Small,
    [CssClass("lg")] Large
}

/// <summary>
/// 通用尺寸。
/// </summary>
public enum Size
{
    [CssClass("sm")] Small,
    [CssClass("lg")] Large
}