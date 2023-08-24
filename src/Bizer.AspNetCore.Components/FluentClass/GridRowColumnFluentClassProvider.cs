namespace Bizer.AspNetCore.Components;

public interface IGridRowColumnBreakPoint : IFluentBreakPoint<IGridRowColumnSize>
{

}

public interface IGridRowColumnSize : IGridRowColumnBreakPoint
{
    public IGridRowColumnBreakPoint Auto { get; }
    public IGridRowColumnBreakPoint Is1 { get; }
    public IGridRowColumnBreakPoint Is2 { get; }
    public IGridRowColumnBreakPoint Is3 { get; }
    public IGridRowColumnBreakPoint Is4 { get; }
    public IGridRowColumnBreakPoint Is5 { get; }
    public IGridRowColumnBreakPoint Is6 { get; }
}

internal class GridRowColumnFluentClassProvider : FluentClassProvider<RowColumns, BreakPoint>, IGridRowColumnSize
{
    public IGridRowColumnBreakPoint Auto => WithSize(RowColumns.Auto);
    public IGridRowColumnBreakPoint Is1 => WithSize(RowColumns.Is1);
    public IGridRowColumnBreakPoint Is2 => WithSize(RowColumns.Is2);
    public IGridRowColumnBreakPoint Is3 => WithSize(RowColumns.Is3);
    public IGridRowColumnBreakPoint Is4 => WithSize(RowColumns.Is4);
    public IGridRowColumnBreakPoint Is5 => WithSize(RowColumns.Is5);
    public IGridRowColumnBreakPoint Is6 => WithSize(RowColumns.Is6);
    public IGridRowColumnSize OnSM => WithBreakPoint(BreakPoint.SM);
    public IGridRowColumnSize OnMD =>WithBreakPoint(BreakPoint.MD);
    public IGridRowColumnSize OnLG =>WithBreakPoint(BreakPoint.LG);
    public IGridRowColumnSize OnXL => WithBreakPoint(BreakPoint.XL);
    public IGridRowColumnSize OnXXL => WithBreakPoint(BreakPoint.XXL);

    IGridRowColumnBreakPoint WithSize(RowColumns rowColumns)
    {
        this.ChangeKey(rowColumns);
        return this;
    }

    IGridRowColumnSize WithBreakPoint(BreakPoint breakPoint)
    {
        this.AddRule(breakPoint);
        return this;
    }

    protected override string? Format(RowColumns key, BreakPoint rule)
    => $"row-cols-{rule.GetCssClass()}-{key.GetCssClass()}";

    protected override string? Format(RowColumns key) => $"row-cols-{key.GetCssClass()}";
}
