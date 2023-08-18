namespace Bizer.AspNetCore.Components;

/// <summary>
/// 具备响应式排版的容器。
/// </summary>
public class Container : BizerChildConentComponentBase, IHasAdditionalClass, IHasAdditionalStyle
{
    /// <summary>
    /// 任何时候都呈现 100% 的宽度。
    /// </summary>
    [Parameter] public bool Fluid { get; set; }

    /// <summary>
    /// 使用 <see cref="Class.Container"/> 调用自适应断点。
    /// </summary>
    [Parameter] public IContainerFluentBreakPoint? BreakPoint { get; set; }

    /// <inheritdoc/>
    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append("container", !Fluid).Append("container-fluid", Fluid);
    }
}
