using Microsoft.JSInterop;

namespace Bizer.AspNetCore.Components;
[CssClass("modal")]
internal class DialogModal : BizerChildConentComponentBase
{
    [Inject] IDialogService DialogService { get; set; }
    [CascadingParameter] DialogContainer DialogContainer { get; set; }
    [Parameter]public DialogConfiguration Configuration { get; set; }
    [Parameter]public DialogParameters Parameters { get; set; }
    [Parameter] public Guid Id { get; set; }

    bool IsOpened { get; set; }

    Dialog? _dialog;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Div("modal").Class("show",IsOpened).Content(base.BuildRenderTree).Close();

        builder.Div("modal-backdrop").Class("show", IsOpened).Close();
    }
    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Open();
        }
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.Component<Dialog>(IsOpened)
            .Attribute(m => m.HeaderContent, _dialog?.HeaderContent)
            .Attribute(m => m.ChildContent, _dialog?.ChildContent)
            .Attribute(m => m.FooterContent, _dialog?.FooterContent)
            .Close();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("modal-dialog-scrollable", Configuration.Scrollable)
            .Append("modal-dialog-centered", Configuration.Centered)
            ;
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["id"] = $"dialog_{Id}";
    }

    /// <summary>
    /// 显示对话框。
    /// </summary>
    public async Task Open()
    {
        IsOpened = true;
    }

    /// <summary>
    /// 关闭对话框。
    /// </summary>
    public async Task Close(DialogResult result)
    {
        IsOpened = false;
        await DialogService.Close(Id, result);
        Reset();
    }

    bool _parameterSet;
    internal void SetDialog(Dialog dialog)
    {
        if (_parameterSet)
        {
            _dialog = dialog;
            _parameterSet = true;
            StateHasChanged();
        }
    }

    internal void Reset() => _parameterSet = false;
}
