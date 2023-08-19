namespace Bizer.AspNetCore.Components;

/// <summary>
/// 表示可折叠的元素。
/// </summary>
[CssClass("collapse")]
public class Collapse : BizerChildConentComponentBase
{
    /// <summary>
    /// 可折叠元素采用横向显示的动画。
    /// </summary>
    [Parameter][CssClass("collapse-horizontal")] public bool Horizontal { get; set; }

    /// <summary>
    /// 设置一个回调函数，表示当可折叠元素展开时触发的事件。
    /// </summary>
    [Parameter] public EventCallback<bool> OnToggle { get; set; }

    /// <summary>
    /// 折叠面板是否处于展开状态。
    /// </summary>
    public bool Expaned { get;private set; }


    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["aria-expanded"] = Expaned.ToString().ToLower();
    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("show", Expaned);        
    }



    /// <summary>
    /// 将可折叠元素状态的切换。
    /// </summary>
    public async Task Toggle()
    {
        Expaned = ! Expaned;
        await OnToggle.InvokeAsync(Expaned);
        await this.Refresh();
    }
}