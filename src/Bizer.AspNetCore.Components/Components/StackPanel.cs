namespace Bizer.AspNetCore.Components;

/// <summary>
/// 自动排列面板。
/// </summary>
public class StackPanel:BizerChildConentComponentBase
{
    /// <summary>
    /// 水平排列。
    /// </summary>
    [Parameter]public bool Horizontal { get; set; }
    /// <summary>
    /// 间隙。
    /// </summary>
    [Parameter] public Gap Gap { get; set; } = Gap.Is3;
    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("vstack", !Horizontal).Append("hstack", Horizontal);
    }
}
