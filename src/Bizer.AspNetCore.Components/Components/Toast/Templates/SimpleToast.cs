using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 简单的提示。
/// </summary>
internal class SimpleToast: ToastTemplateBase
{
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
