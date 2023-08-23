using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 验证表单。
/// </summary>
[ParentComponent]
[HtmlTag("form")]
public class Form : BizerComponentBase, IFormComponent
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]public object? Model { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]public EventCallback<EditContext> OnSubmit { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]public EventCallback<EditContext> OnValidSubmit { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]public EventCallback<EditContext> OnInvalidSubmit { get; set; }
    public EditContext? FixedEditContext { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]public EditContext? EditContext { get; set; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public RenderFragment<EditContext>? ChildContent { get; set; }
}
