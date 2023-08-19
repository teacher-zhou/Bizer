namespace Bizer.AspNetCore.Components;

[ChildComponent(typeof(ButtonGroup), Optional = true)]
[ParentComponent]
public class DropDown:BizerComponentBase
{
    [CascadingParameter]public ButtonGroup? CascadingButtonGroup { get; set; }
    /// <summary>
    /// 暗色对比度时使用。
    /// </summary>
    [Parameter]public bool DarkContrast { get; set; }

    /// <summary>
    /// 触发下拉菜单的内容。
    /// </summary>
    [Parameter]public RenderFragment? ToggleContent { get; set; }
    /// <summary>
    /// 菜单的内容。
    /// </summary>
    [Parameter]public RenderFragment? MenuContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if(CascadingButtonGroup is null)
        {
            base.BuildRenderTree(builder);
        }
        else
        {
            builder.CreateCascadingComponent(this, 0, content =>
            {
                AddContent(content, 0);
            });
        }
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.Content(ToggleContent);

        builder.Element("ul", "dropdown-menu")
            .Class("dropdown-menu-dark",DarkContrast)
            .Content(MenuContent)
            .Close();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        if (CascadingButtonGroup is null)
        {
            builder.Append("dropdown");
        }
    }
}
