using System.Text.Json;
using Bizer.AspNetCore.Components.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Bizer.AspNetCore.Components;
[CssClass("modal")]
internal class DialogRenderer : BizerChildConentComponentBase
{
    public DialogRenderer()
    {
        CaptureReference = true;
    }

    [Inject] IDialogService DialogService { get; set; }
    [CascadingParameter] DialogContainer DialogContainer { get; set; }
    [Parameter]public DialogConfiguration Configuration { get; set; }
    [Parameter]public DynamicParameters Parameters { get; set; }
    [Parameter] public Guid Id { get; set; }
    

    Dialog? _dialog;
    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ImportBizerJsModuleAsync();
            await Open();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateCascadingComponent(this, 0, modal =>
        {
            modal.CreateComponent<DialogContext>(0, base.BuildRenderTree);
        });
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.Div("modal-dialog")
            .Class("modal-dialog-scrollable", Configuration.Scrollable)
            .Class("modal-dialog-centered", Configuration.Centered)
            .Content(content => content.Div("modal-content")
                                    .Content(dialog =>
                                    {
                                        if(_dialog is null)
                                        {
                                            return;
                                        }
                                        dialog.Div("modal-header", _dialog.HeaderContent is not null)
                                                .Content(content =>
                                                {
                                                    content.Element("h1", "modal-title").Class("fs-5").Content(_dialog.HeaderContent).Close();

                                                    content.Component<Button>(_dialog.Closable).Class("btn-close").Close();
                                                })
                                                .Close();

                                        dialog.Div("modal-body").Content(_dialog.ChildContent).Close();

                                        dialog.Div("modal-footer").Content(_dialog.FooterContent).Close();

                                    })
                                    .Close())
            .Close();
    }
    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("fade");
    }

    /// <summary>
    /// 显示对话框。
    /// </summary>
    public async Task Open()
    {
        await BizerJsModule!.Module.InvokeVoidAsync("modal.open", Reference, Configuration, DotNetObjectReference.Create(this));
    }

    /// <summary>
    /// 关闭对话框。
    /// </summary>
    public async Task Close(DialogResult result)
    {
        await BizerJsModule!.Module.InvokeVoidAsync("modal.close", Reference, Configuration, DotNetObjectReference.Create(this));
        await Task.Delay(50);

        await DialogService.Close(Id, result);
        //Reset();
    }

    bool _parameterSet;
    internal void SetDialog(Dialog dialog)
    {
        if (!_parameterSet)
        {
            _dialog = dialog;
            _parameterSet = true;
            StateHasChanged();
        }
    }

    #region JS Callback
    [JSInvokable("OnOpeningAsync")]
    public Task JsInvokeOnOpeningAsync() => Configuration.OnOpening.NullableInvoke();

    [JSInvokable("OnOpenedAsync")]
    public Task JsInvokeOnOpenedAsync() => Configuration.OnOpened.NullableInvoke();
    [JSInvokable("OnClosingAsync")]
    public Task JsInvokeOnClosingAsync() => Configuration.OnClosing.NullableInvoke();

    [JSInvokable("OnClosedAsync")]
    public Task JsInvokeOnClosedAsync() => Configuration.OnClosed.NullableInvoke();
    #endregion
}
