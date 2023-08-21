namespace Bizer.AspNetCore.Components;
/// <summary>
/// 对话框。
/// </summary>
public class Dialog : ComponentBase
{
    /// <summary>
    /// 上下文。
    /// </summary>
    [CascadingParameter]DialogContext Context { get; set; }
    /// <summary>
    /// 对话框消息的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? ChildContent { get; set; }
    /// <summary>
    /// 对话框标题的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? HeaderContent { get; set; }
    /// <summary>
    /// 对话框用于操作的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? FooterContent { get; set; }

    [Parameter]public bool Closable { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Div("modal-header", HeaderContent is not null)
            .Content(content =>
            {
                content.Element("h1", "modal-title").Class("fs-5").Content(HeaderContent).Close();

                content.Component<Button>(Closable).Class("btn-close").Close();
            })
            .Close();

        builder.Div("modal-body").Content(ChildContent).Close();

        builder.Div("modal-footer").Content(FooterContent).Close();
    }

    protected override void OnInitialized()
    {
        Context?.Register(this);
    }
}