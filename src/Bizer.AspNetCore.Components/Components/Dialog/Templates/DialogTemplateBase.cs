using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components.Templates;

/// <summary>
/// 表示对话框模板的基类。
/// </summary>
public abstract class DialogTemplateBase : ComponentBase
{
    /// <summary>
    /// 用于操作的对话框上下文。
    /// </summary>
    [CascadingParameter]protected DialogContext Context { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Component<Dialog>()
            .Attribute(nameof(Dialog.HeaderContent), Context.Parameters.GetTitle())
            .Attribute(nameof(Dialog.ChildContent), Context.Parameters.GetContent())
            .Attribute(nameof(Dialog.FooterContent), BuildFooter())
            .Close();
    }

    /// <summary>
    /// 构建对话框的底部。
    /// </summary>
    /// <returns></returns>
    protected abstract RenderFragment? BuildFooter();
}
