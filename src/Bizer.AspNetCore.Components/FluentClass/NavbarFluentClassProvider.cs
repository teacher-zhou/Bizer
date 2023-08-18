namespace Bizer.AspNetCore.Components;

public interface INavbarFluentBreakPoint : IFluentBreakPoint<INavbarFluentBreakPoint> { }

internal class NavbarFluentClassProvider:FluentClassProvider<BreakPoint, string>, INavbarFluentBreakPoint
{
    public INavbarFluentBreakPoint OnSM => WithBreakPoint(BreakPoint.SM);
    public INavbarFluentBreakPoint OnMD => WithBreakPoint(BreakPoint.MD);
    public INavbarFluentBreakPoint OnLG => WithBreakPoint(BreakPoint.LG);
    public INavbarFluentBreakPoint OnXL => WithBreakPoint(BreakPoint.XL);
    public INavbarFluentBreakPoint OnXXL => WithBreakPoint(BreakPoint.XXL);

    protected override string? Format(BreakPoint key, string value)
    => $"navbar-expand-{value}";

    protected override string? Format(BreakPoint key)
    => $"navbar-expand-{key.GetCssClass()}";

    INavbarFluentBreakPoint WithBreakPoint(BreakPoint breakPoint)
    {
        AddRule(breakPoint.GetCssClass());
        return this;
    }
}
