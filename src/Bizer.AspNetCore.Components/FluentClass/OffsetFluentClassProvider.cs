namespace Bizer.AspNetCore.Components;

public interface IOffsetBreakPoint : IFluentBreakPoint<IOffsetSize>,IFluentAndClass<IOffsetSize>
{
}

public interface IOffsetSize : IOffsetBreakPoint
{
    IOffsetBreakPoint Is1 { get; }
    IOffsetBreakPoint Is2 { get; }
    IOffsetBreakPoint Is3 { get; }
    IOffsetBreakPoint Is4 { get; }
    IOffsetBreakPoint Is5 { get; }
    IOffsetBreakPoint Is6 { get; }
    IOffsetBreakPoint Is7 { get; }
    IOffsetBreakPoint Is8 { get; }
    IOffsetBreakPoint Is9 { get; }
    IOffsetBreakPoint Is10 { get; }
    IOffsetBreakPoint Is11 { get; }

}

internal class OffsetFluentClassProvider : FluentClassProvider<int, BreakPoint?>, IOffsetSize
{
    public IOffsetBreakPoint Is1 => WithSize(1);
    public IOffsetBreakPoint Is2 => WithSize(2);
    public IOffsetBreakPoint Is3 => WithSize(3);
    public IOffsetBreakPoint Is4 => WithSize(4);
    public IOffsetBreakPoint Is5 => WithSize(5);
    public IOffsetBreakPoint Is6 => WithSize(6);
    public IOffsetBreakPoint Is7 => WithSize(7);
    public IOffsetBreakPoint Is8 => WithSize(8);
    public IOffsetBreakPoint Is9 => WithSize(9);
    public IOffsetBreakPoint Is10=> WithSize(10);
    public IOffsetBreakPoint Is11 => WithSize(11);
    public IOffsetSize OnSM => WithBreakPoint(BreakPoint.SM);
    public IOffsetSize OnMD => WithBreakPoint(BreakPoint.MD);
    public IOffsetSize OnLG => WithBreakPoint(BreakPoint.LG);
    public IOffsetSize OnXL => WithBreakPoint(BreakPoint.XL);
    public IOffsetSize OnXXL => WithBreakPoint(BreakPoint.XXL);

    public IOffsetSize And => WithBreakPoint(default);

    IOffsetBreakPoint WithSize(int size)
    {
        this.ChangeKey(size);
        return this;
    }

    IOffsetSize WithBreakPoint(BreakPoint? breakPoint)
    {
        if (breakPoint.HasValue)
        {
            this.AddRule(breakPoint.Value);
        }
        return this;
    }

    protected override string? Format(int key, BreakPoint? rule)
    => $"offset{rule?.GetCssClass("-")}-{key.GetCssClass()}";

    protected override string? Format(int key) => $"offset-{key.GetCssClass()}";
}
