namespace Bizer.AspNetCore.Components.Templates;

/// <summary>
/// 只有一个【确定】按钮的消息提示对话框。
/// </summary>
public class MessageDialogTemplate : DialogTemplateBase
{

    /// <inheritdoc/>
    protected override RenderFragment? BuildFooter()
        => builder => builder.Component<Button>()
                                .Attribute(m => m.Color, Color.Primary)
                                .Content("确定")
                                .Callback<MouseEventArgs>("onclick", this, e => Context.Confirm(true))
                            .Close()

        ;
}
