using Microsoft.JSInterop;

namespace Bizer.AspNetCore.Components;

[CssClass("toast")]
[HtmlRole("alert")]
internal class ToastRenderer:BizerComponentBase
{
    public ToastRenderer()
    {
        CaptureReference = true;
    }
    [CascadingParameter]ToastContainer Container { get; set; }
    [CascadingParameter] ToastConfiguration Configuration { get; set; }
    [Parameter] public Func<ToastConfiguration, Task> ClosedHandler { get; set; }

    Toast _toast;

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ImportBizerJsModuleAsync();
            await Show();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateCascadingComponent(this, 0, content =>
        {
            content.Component<ToastContext>().Content(base.BuildRenderTree).Close();
        });
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        if(_toast is null)
        {
            return;
        }
        builder.Div("toast-header", _toast.HeaderContent is not null )
            .Content(header =>
            {
                header.Content(_toast.HeaderContent);

                //header.Component<Button>(_toast.Closable).Class("btn-close").Callback<MouseEventArgs>("onclick", this, e => ClosedHandler(Configuration)).Close();
            })
            .Close();

        builder.Div("toast-body").Content(_toast.ChildContent).Close();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("fade");
    }
    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["aria-live"] = "assertive";
        attributes["aria-atomic"] = "true";
    }

    /// <summary>
    /// 显示对话框。
    /// </summary>
    public async Task Show()
    {
        await BizerJsModule!.Module.InvokeVoidAsync("toast.show", Reference, Configuration);
    }

    /// <summary>
    /// 关闭对话框。
    /// </summary>
    public async Task Close()
    {
        await BizerJsModule!.Module.InvokeVoidAsync("toast.hide");
        await Task.Delay(50);
        await ClosedHandler(Configuration);       
    }

    bool _parameterSet;
    internal void Set(Toast toast)
    {
        if (!_parameterSet)
        {
            _toast = toast;
            _parameterSet = true;
            StateHasChanged();
        }
    }
}
