using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizer.AspNetCore.Components;



public interface IGridColumnSizeBreakPoint : IFluentBreakPoint<IGridColumnSize>, IFluentAndClass<IGridColumnSize>
{

}
public interface IGridColumnSize
{
    public IGridColumnSizeBreakPoint Is1 { get; }
    public IGridColumnSizeBreakPoint Is2 { get; }
    public IGridColumnSizeBreakPoint Is3 { get; }
    public IGridColumnSizeBreakPoint Is4 { get; }
    public IGridColumnSizeBreakPoint Is5 { get; }
    public IGridColumnSizeBreakPoint Is6 { get; }
    public IGridColumnSizeBreakPoint Is7 { get; }
    public IGridColumnSizeBreakPoint Is8 { get; }
    public IGridColumnSizeBreakPoint Is9 { get; }
    public IGridColumnSizeBreakPoint Is10 { get; }
    public IGridColumnSizeBreakPoint Is11 { get; }
    public IGridColumnSizeBreakPoint Is12 { get; }
    public IGridColumnSizeBreakPoint Auto { get; }
}

internal class GridColumnFluentClassProvider :FluentClassProvider<Columns,BreakPoint?>, IGridColumnSize, IGridColumnSizeBreakPoint
{
    public IGridColumnSizeBreakPoint Is1 => WithSize(Columns.Is1);
    public IGridColumnSizeBreakPoint Is2 => WithSize(Columns.Is2);
    public IGridColumnSizeBreakPoint Is3 => WithSize(Columns.Is3);
    public IGridColumnSizeBreakPoint Is4 => WithSize(Columns.Is4);
    public IGridColumnSizeBreakPoint Is5 => WithSize(Columns.Is5);
    public IGridColumnSizeBreakPoint Is6 => WithSize(Columns.Is6);
    public IGridColumnSizeBreakPoint Is7 => WithSize(Columns.Is7);
    public IGridColumnSizeBreakPoint Is8 => WithSize(Columns.Is8);
    public IGridColumnSizeBreakPoint Is9 => WithSize(Columns.Is9);
    public IGridColumnSizeBreakPoint Is10 => WithSize(Columns.Is10);
    public IGridColumnSizeBreakPoint Is11 => WithSize(Columns.Is11);
    public IGridColumnSizeBreakPoint Is12 => WithSize(Columns.Is12);
    public IGridColumnSizeBreakPoint Auto => WithSize(Columns.Auto);
    public IGridColumnSize OnSM =>WithBreakPoint(BreakPoint.SM);
    public IGridColumnSize OnMD =>WithBreakPoint(BreakPoint.MD);
    public IGridColumnSize OnLG =>WithBreakPoint(BreakPoint.LG);
    public IGridColumnSize OnXL =>WithBreakPoint(BreakPoint.XL);
    public IGridColumnSize OnXXL => WithBreakPoint(BreakPoint.XXL);

    public IGridColumnSize And => WithBreakPoint(default);

    IGridColumnSizeBreakPoint WithSize(Columns columns)
    {
        this.ChangeKey(columns);
        return this;
    }

    IGridColumnSize WithBreakPoint(BreakPoint? breakPoint)
    {
        if (breakPoint.HasValue)
        {
            this.AddRule(breakPoint);
        }
        return this;
    }

    protected override string? Format(Columns key, BreakPoint? rule)
    => $"col{rule?.GetCssClass("-")}-{key.GetCssClass()}";

    protected override string? Format(Columns key) => $"col-{key.GetCssClass()}";
}
