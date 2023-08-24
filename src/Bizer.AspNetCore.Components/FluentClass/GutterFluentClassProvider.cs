using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizer.AspNetCore.Components;

public interface IGutterBreakPoint:IFluentBreakPoint<IGutterSize>,IFluentAndClass<IGutterSideWithBreakPoint>
{

}

public interface IGutterSizeWithBreakPoint : IGutterSize, IGutterBreakPoint
{

}

public interface IGutterSideWithBreakPoint : IGutterSide, IGutterSizeWithBreakPoint
{

}

public interface IGutterSize
{
    IGutterSideWithBreakPoint Is0 { get; }
    IGutterSideWithBreakPoint Is1 { get; }
    IGutterSideWithBreakPoint Is2 { get; }
    IGutterSideWithBreakPoint Is3 { get; }
    IGutterSideWithBreakPoint Is4 { get; }
    IGutterSideWithBreakPoint Is5 { get; }
}

public interface IGutterSide
{
    IGutterSizeWithBreakPoint FromHorizontal { get; }
    IGutterSizeWithBreakPoint FromVertical { get; }
}

internal class GutterFluentClassProvider : FluentClassProvider<Gap, (string? side, BreakPoint? breakPoint)>, IGutterSizeWithBreakPoint, IGutterSideWithBreakPoint
{
    private (string? side, BreakPoint? breakPoint) definition = new();

    public IGutterSideWithBreakPoint Is0 =>WithSize(Gap.Is0);
    public IGutterSideWithBreakPoint Is1 =>WithSize(Gap.Is1);
    public IGutterSideWithBreakPoint Is2 =>WithSize(Gap.Is2);
    public IGutterSideWithBreakPoint Is3 =>WithSize(Gap.Is3);
    public IGutterSideWithBreakPoint Is4 =>WithSize(Gap.Is4);
    public IGutterSideWithBreakPoint Is5 => WithSize(Gap.Is5);
    public IGutterSize OnSM => WithBreakPoint(BreakPoint.SM);
    public IGutterSize OnMD =>WithBreakPoint(BreakPoint.MD);
    public IGutterSize OnLG =>WithBreakPoint(BreakPoint.LG);
    public IGutterSize OnXL =>WithBreakPoint(BreakPoint.XL);
    public IGutterSize OnXXL=>WithBreakPoint(BreakPoint.XXL);
    public IGutterSideWithBreakPoint And { get; }
    public IGutterSizeWithBreakPoint FromHorizontal => WithSide("x");
    public IGutterSizeWithBreakPoint FromVertical => WithSide("y");

    IGutterSideWithBreakPoint WithSize(Gap size)
    {
        this.ChangeKey(size);
        return this;
    }

    IGutterSize WithBreakPoint(BreakPoint? breakPoint)
    {
        definition.breakPoint = breakPoint;
        return this;
    }

    IGutterSizeWithBreakPoint WithSide(string side)
    {
        definition.side = side;
        AddRule(definition); 
        return this;
    }

    protected override string? Format(Gap key, (string? side, BreakPoint? breakPoint) rule)
    {
        var builder = new StringBuilder($"gap");
        if(!string.IsNullOrEmpty(rule.side))
        {
            builder.Append(rule.side);
        }
        if (rule.breakPoint != null)
        {
            builder.Append(rule.breakPoint.GetCssClass("-"));
        }
        return builder.ToString();
    }

    protected override string? Format(Gap key) => $"gap-{key.GetCssClass()}";
}