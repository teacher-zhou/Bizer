using ComponentBuilder.Definitions;
using ComponentBuilder.JSInterop;

using Microsoft.JSInterop;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示组件库的基类。
/// </summary>
public abstract class BizerComponentBase : BlazorComponentBase, IHasAdditionalStyle, IHasAdditionalClass
{
    [Inject] protected BizerComponentOptions Options { get; set; }
    [Inject] protected IJSRuntime JS { get; set; }
    /// <inheritdoc/>
    [Parameter] public string? AdditionalStyle { get; set; }
    /// <inheritdoc/>
    [Parameter] public string? AdditionalClass { get; set; }

    protected bool EnableImportJS { get; set; }

    protected IJSModule? BizerJsModule { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (EnableImportJS)
            {
                BizerJsModule = await JS.ImportBizerComponentAsync();
            }
        }
    }


    protected ValueTask InvokeVoidAsync(string identifier, params object?[] args)
    {
        Checker.NotNullOrWhiteSpace(identifier, nameof(identifier));

        if (BizerJsModule is null)
        {
            throw new InvalidOperationException($"请先设置 {nameof(EnableImportJS)} 为 true");
        }
        return BizerJsModule.Module.InvokeVoidAsync(identifier, args);
    }

    protected ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[] args)
    {
        Checker.NotNullOrWhiteSpace(identifier, nameof(identifier));

        if (BizerJsModule is null)
        {
            throw new InvalidOperationException($"请先设置 {nameof(EnableImportJS)} 为 true");
        }
        return BizerJsModule.Module.InvokeAsync<TValue>(identifier, args);
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
