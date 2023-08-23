namespace Bizer.AspNetCore.Components;


/// <summary>
/// 提示渲染器。
/// </summary>
[CssClass("toast")]
[HtmlRole("alert")]
internal class ToastRenderer:BizerComponentBase
{
    [CascadingParameter] ToastConfiguration Configuration { get; set; }
    [Parameter] public Func<ToastConfiguration, Task> ClosedHandler { get; set; }
    Toast _toast;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateCascadingComponent(this, 0, content =>
        {
            content.Component<ToastContext>().Content(base.BuildRenderTree).Close();
        });
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        if (_toast is null)
        {
            return;
        }
        builder.Div("toast-header", _toast.HeaderContent is not null)
            .Content(header =>
            {
                header.Content(_toast.HeaderContent);

                header.Component<Button>(_toast.Closable).Class("btn-close").Callback<MouseEventArgs>("onclick", this, e => ClosedHandler(Configuration)).Close();
            })
            .Close();

        builder.Div("toast-body").Content(_toast.ChildContent).Close();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("show").Append("mt-2");

        var filledColor = Configuration.Parameters.Get<bool>("ColorFilled");
        if (filledColor)
        {
            builder.Append($"text-bg-{Configuration.Parameters.Get<Color?>("Color")}");
        }
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["aria-live"] = "assertive";
        attributes["aria-atomic"] = "true";
    }

    bool _parameterSet;
    internal void Set(Toast toast)
    {
        if (!_parameterSet)
        {
            _toast = toast;
            _parameterSet = true;
            StateHasChanged();
        }
    }
}
