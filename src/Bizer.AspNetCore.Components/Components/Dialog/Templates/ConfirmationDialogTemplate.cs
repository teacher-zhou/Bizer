namespace Bizer.AspNetCore.Components.Templates;

/// <summary>
/// 拥有【确定】和【取消】按钮的确认对话框模板。
/// </summary>
public class ConfirmationDialogTemplate : DialogTemplateBase
{
    /// <inheritdoc/>
    protected override RenderFragment? BuildFooter()
        => builder => builder.Component<Button>()
                                .Attribute(m => m.Color, Color.Light)
                                .Attribute(m => m.Outline, true)
                                .Callback<MouseEventArgs>("onclick", this, e => Context.Cancel())
                                .Content("取消")
                            .Close()
                            .Component<Button>()
                                .Attribute(m => m.Color, Color.Primary)
                                .Callback<MouseEventArgs>("onclick", this, e => Context.Confirm(true))
                                .Content("确定")
                            .Close()

        ;
}
