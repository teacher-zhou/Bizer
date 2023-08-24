﻿namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表单行布局。
/// </summary>
[ChildComponent(typeof(Form))]
[ParentComponent]
[CssClass("row")]
public class FormRow : BizerChildConentComponentBase
{
    [CascadingParameter] public Form Form { get; set; }
    /// <summary>
    /// 行间隔。
    /// </summary>
    [Parameter][CssClass("mb-")] public Space? Space { get; set; } = Components.Space.Is3;

    /// <summary>
    /// 行内布局。
    /// </summary>
    [Parameter] public bool Inline { get; set; }
}