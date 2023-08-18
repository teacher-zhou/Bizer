namespace Bizer.AspNetCore.Components;
public interface IContainerFluentBreakPoint : IFluentBreakPoint<IContainerFluentBreakPoint>
{
}

internal class ContainerFluentClassProvider : FluentClassProvider<BreakPoint, string>, IContainerFluentBreakPoint
{
    public IContainerFluentBreakPoint OnSM => WithBreakPoint(BreakPoint.SM);
    public IContainerFluentBreakPoint OnMD => WithBreakPoint(BreakPoint.MD);
    public IContainerFluentBreakPoint OnLG => WithBreakPoint(BreakPoint.LG);
    public IContainerFluentBreakPoint OnXL => WithBreakPoint(BreakPoint.XL);
    public IContainerFluentBreakPoint OnXXL => WithBreakPoint(BreakPoint.XXL);

    protected override string? Format(BreakPoint key, string value)
    => $"container-{value}";

    protected override string? Format(BreakPoint key)
    => $"container-{key.GetCssClass()}";

    IContainerFluentBreakPoint WithBreakPoint(BreakPoint breakPoint)
    {
        AddRule(breakPoint.GetCssClass());
        return this;
    }
}
