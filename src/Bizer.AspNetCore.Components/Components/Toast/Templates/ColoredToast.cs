using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 有背景颜色的提示。
/// </summary>
internal class ColoredToast:ToastTemplateBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Component<Toast>()
            .Content(Context.Parameters.GetContent())
            .Close();
    }
}
