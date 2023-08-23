using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 提示上下文。
/// </summary>
public class ToastContext:ComponentBase
{
    /// <summary>
    /// 获取参数。
    /// </summary>
    public DynamicParameters Parameters => Configuration.Parameters;
    /// <summary>
    /// 配置。
    /// </summary>
    [CascadingParameter]ToastConfiguration Configuration { get; set; }
    [Parameter]public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 关闭提示。
    /// </summary>
    /// <returns></returns>
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
