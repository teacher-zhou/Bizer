using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

internal class SimpleToast:ComponentBase
{
    /// <summary>
    /// 用于操作的对话框上下文。
    /// </summary>
    [CascadingParameter] protected ToastContext Context { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Component<Toast>()
            .Attribute(m => m.HeaderContent, content =>
            {
                content.Span("rounded me-2 p-2").Class(Context.Parameters.Get<Color?>("Color")?.GetCssClass("bg-")).Close();
                content.Element("strong", "me-auto").Content(Context.Parameters.GetTitle()).Close();
            })
            .Content(Context.Parameters.GetContent())            
            .Close();
    }
}
