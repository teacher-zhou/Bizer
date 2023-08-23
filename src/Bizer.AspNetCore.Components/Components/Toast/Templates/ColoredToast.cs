using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

internal class ColoredToast:ComponentBase
{    /// <summary>
     /// 用于操作的对话框上下文。
     /// </summary>
    [CascadingParameter] protected ToastContext Context { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.Component<Toast>()
            .Content(Context.Parameters.GetContent())
            .Close();
    }
}
