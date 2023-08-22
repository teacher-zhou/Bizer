using Microsoft.JSInterop;
using ComponentBuilder.JSInterop;
using ComponentBuilder.Definitions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示组件库的基类。
/// </summary>
[ChildComponent(typeof(DropDown),Optional =true)]
public abstract class BizerComponentBase : BlazorComponentBase, IHasAdditionalStyle, IHasAdditionalClass
{
    [Inject] protected BizerComponentOptions Options { get; set; }
    [Inject] protected IJSRuntime JS { get; set; }

    [CascadingParameter]public DropDown? CascadingDropDown { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? AdditionalStyle { get; set; }
    /// <inheritdoc/>
    [Parameter] public string? AdditionalClass { get; set; }
    /// <summary>
    /// BS 脚本触发的目标。
    /// </summary>
    [Parameter][HtmlData("bs-target")] public string? BsTarget { get; set; }
    /// <summary>
    /// BS 触发的脚本值。
    /// </summary>
    [Parameter][HtmlData("bs-toggle")] public Toggle? BsToggle { get; set; }
    /// <summary>
    /// 释放模式。
    /// </summary>
    [Parameter][HtmlData("bs-dismiss")]public Dismiss? BsDismiss { get; set; }
    /// <summary>
    /// aria-labelledby
    /// </summary>
    [Parameter][HtmlAria("labelledby")] public string? AriaLabelledBy { get; set; }
    /// <summary>
    /// aria-label
    /// </summary>
    [Parameter][HtmlAria("label")] public string? AriaLabel { get; set; }
    /// <summary>
    /// aria-hidden
    /// </summary>
    [Parameter][HtmlAria("hidden")]public bool AriaHidde { get; set; }

    /// <summary>
    /// Bizer 的 js 模块，需要手动调用 <see cref="ImportBizerJsModuleAsync"/> 方法后才可以使用。
    /// </summary>
    protected IJSModule? BizerJsModule { get; private set; }

    /// <summary>
    /// 阻止下拉框的 class 生成。
    /// </summary>
    protected bool PreventDropDownToggleClass { get;set; }

    /// <summary>
    /// 重写支持 <see cref="DropDown"/> 组件。
    /// </summary>
    protected virtual bool CanDropDown => !PreventDropDownToggleClass && CascadingDropDown is not null;

    protected override void AfterSetParameters(ParameterView parameters)
    {
        base.AfterSetParameters(parameters);

        if(CanDropDown)
        {
            BsToggle = Toggle.Dropdown;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ImportBizerJsModuleAsync();
        }
    }

    /// <summary>
    /// 导入 bizer.js 模块。
    /// </summary>
    protected async Task ImportBizerJsModuleAsync()
    {
        BizerJsModule = await JS.ImportBizerComponentAsync();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        if(CanDropDown)
        {
            builder.Append("dropdown-toggle");
        }
    }
}

/// <summary>
/// 表示具备任意 UI 内容的组件基类。
/// </summary>
public abstract class BizerChildConentComponentBase : BizerComponentBase, IHasChildContent
{
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
