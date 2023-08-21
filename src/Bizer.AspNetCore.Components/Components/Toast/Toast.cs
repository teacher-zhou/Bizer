using ComponentBuilder.FluentRenderTree;

namespace Bizer.AspNetCore.Components;

[CssClass("toast")]
[HtmlRole("alert")]
internal class Toast : BlazorComponentBase
{
    [Parameter] public ToastConfiguration Configuration { get; set; }
    [Parameter] public Func<ToastConfiguration, Task>? CloseHandler { get; set; }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.Div("toast-header",Configuration.HeaderTemplate is not null || !string.IsNullOrEmpty( Configuration.Title))
            .Content(header =>
            {
                Configuration.HeaderTemplate ??= x => x
                                                .Span(Configuration.Color.GetCssClass("bg-"))
                                                    .Style("width:20px;height:20px")
                                                    .Class("rounded me-2")
                                                .Element("strong", "me-auto").Content(Configuration.Title)
                                                .Close()
                                                ;
                header.Content(Configuration.HeaderTemplate);

                header.Component<Button>(Configuration.Closable).Class("btn-close").Callback<MouseEventArgs>("onclick",this,e=>CloseHandler?.Invoke(Configuration)).Close();
            })
            .Close();

        builder.Div("toast-body").Content(Configuration.GetBody()).Close();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("mt-3").Append("fade").Append("show");
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["aria-live"] = "assertive";
        attributes["aria-atomic"] = "true";
    }
}
