using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

internal class OperationToast : ToastTemplateBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var color = Context.Parameters.Get<Color?>("Color");
        IconName? icon = color switch
        {
            Color.Primary => IconName.InfoCircle,
            Color.Info => IconName.InfoCircle,
            Color.Success => IconName.CheckCircle,
            Color.Warning => IconName.ExclamtionCircle,
            Color.Danger => IconName.XCircle,
            _ => default
        };

        builder.Component<Toast>()
            .Attribute(m => m.ChildContent, content =>
            {
                content.Component<Icon>(icon.HasValue)
                            .Attribute(m => m.Name, icon.Value)
                            .Attribute(m => m.AdditionalClass, HtmlHelper.Instance.Class().Append(color?.GetCssClass("text-")).Append("me-2").ToString())
                            .Attribute(m => m.AdditionalStyle, "font-size:1.2rem;")
                            .Close();
                content.Element("span", "align-text-bottom fs-6").Content(Context.Parameters.GetContent()).Close();
            })
            .Close();
    }
}
