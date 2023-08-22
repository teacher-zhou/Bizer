using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public class ToastContext:ComponentBase
{

    public DynamicParameters Parameters => Configuration.Parameters;
    [CascadingParameter]public ToastConfiguration Configuration { get; set; }
    [Parameter]public RenderFragment? ChildContent { get; set; }

    public Task Close() => Parameters.GetCloseToastHandler().NullableInvoke(Configuration);

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateCascadingComponent(this, 0, content =>
        {
            var template = Parameters.GetDynamicTemplate();

            content.OpenComponent(0, template);
            content.CloseComponent();

            content.AddContent(1, ChildContent);
        });
    }
}
