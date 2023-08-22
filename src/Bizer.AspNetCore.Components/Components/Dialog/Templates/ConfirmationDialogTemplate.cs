namespace Bizer.AspNetCore.Components.Templates;

/// <summary>
/// 拥有【确定】和【取消】按钮的确认对话框模板。
/// </summary>
public class ConfirmationDialogTemplate : DialogTemplateBase
{
    /// <inheritdoc/>
    protected override RenderFragment? BuildFooter()
        => builder => builder.Component<Button>()
                                .Attribute(m => m.Color, Color.Secondary)
                                .Attribute(m => m.Outline, true)
                                .Content("取消")
                                .Callback<MouseEventArgs>("onclick", this, e => Context.Cancel())
                            .Component<Button>()
                                .Attribute(m => m.Color, Color.Primary)
                                .Content("确定")
                                .Callback<MouseEventArgs>("onclick", this, e => Context.Confirm(true))
                            .Close()

        ;
}
