using System.ComponentModel;
using System.Text;

namespace Bizer.AspNetCore.Components;
public interface ISpacingBreakPoint:IFluentBreakPoint<ISpacingSize>, IFluentAndClass<ISpacingSize>
{
}

public interface ISpacingSides: ISpacingBreakPoint
{
    ISpacingBreakPoint FromTop { get; }
    ISpacingBreakPoint FromBottom { get; }
    ISpacingBreakPoint FromStart { get; }
    ISpacingBreakPoint FromEnd { get; }
    ISpacingBreakPoint FromX { get; }
    ISpacingBreakPoint FromY { get; }
}

public interface ISpacingSize:IFluentAndClass
{
    ISpacingSides IsAuto { get; }
    ISpacingSides Is0 { get; }
    ISpacingSides Is1 { get; }
    ISpacingSides Is2 { get; }
    ISpacingSides Is3 { get; }
    ISpacingSides Is4 { get; }
    ISpacingSides Is5 { get; }
}

public interface ISpacingFluentClass: ISpacingSize, ISpacingSides, ISpacingBreakPoint,IFluentAndClass
{

}

internal abstract class SpacingFluentClassProvider : FluentClassProvider<Space, SpaceDefinition>, ISpacingFluentClass
{
    private readonly Space _space;
    private SpaceDefinition _spaceDefinition = new();

    protected SpacingFluentClassProvider(Space space)
    {
        _space = space;
    }

    public ISpacingSize OnSM => WithSize(BreakPoint.SM);
    public ISpacingSize OnMD => WithSize(BreakPoint.MD);
    public ISpacingSize OnLG => WithSize(BreakPoint.LG);
    public ISpacingSize OnXL => WithSize(BreakPoint.XL);
    public ISpacingSize OnXXL => WithSize(BreakPoint.XXL);
    public ISpacingSize And => WithSize(default);
    public ISpacingBreakPoint FromTop => WithBreakPoint("t");
    public ISpacingBreakPoint FromBottom => WithBreakPoint("b");
    public ISpacingBreakPoint FromStart => WithBreakPoint("s");
    public ISpacingBreakPoint FromEnd => WithBreakPoint("e");
    public ISpacingBreakPoint FromX => WithBreakPoint("x");
    public ISpacingBreakPoint FromY => WithBreakPoint("y");
    public ISpacingSides IsAuto => WithSide(default);
    public ISpacingSides Is0 => WithSide(0);
    public ISpacingSides Is1 => WithSide(1);
    public ISpacingSides Is2 => WithSide(2);
    public ISpacingSides Is3 => WithSide(3);
    public ISpacingSides Is4 => WithSide(4);
    public ISpacingSides Is5 => WithSide(5);
    
    ISpacingSides WithSide(int? size)
    {
        if ( size.HasValue )
        {
            _spaceDefinition.Size = size!.ToString()!;
        }
        else
        {
            _spaceDefinition.Size = "auto";
        }

        AddRule(_spaceDefinition);
        return this;
    }

    ISpacingSize WithSize(BreakPoint? breakPoint)
    {
        _spaceDefinition.BreakPoint = breakPoint;
        return this;
    }

    ISpacingBreakPoint WithBreakPoint(string? side)
    {
        _spaceDefinition.Side = side;
        return this;
    }
    protected override string? Format(Space key)
        => Format(key, _spaceDefinition);

    protected override string? Format(Space key, SpaceDefinition value)
    {
        var builder = new StringBuilder(key.GetDefaultValue()!.ToString());
        if(!string.IsNullOrWhiteSpace(value.Side) )
        {
            builder.Append(value.Side);
        }
        if ( value.BreakPoint.HasValue )
        {
            builder.AppendFormat("-{0}",value.BreakPoint.Value.GetCssClass());
        }
        builder.Append($"-{value.Size}");
        return builder.ToString();
    }
}

internal class MarginFluentClassProvider : SpacingFluentClassProvider, ISpacingFluentClass
{
    public MarginFluentClassProvider() : base(Space.Margin)
    {
    }
}
internal class PaddingFluentClassProvider : SpacingFluentClassProvider, ISpacingFluentClass
{
    public PaddingFluentClassProvider() : base(Space.Padding)
    {
    }
}

internal struct SpaceDefinition
{
    public string Size;
    public string? Side;
    public BreakPoint? BreakPoint;
}

internal enum Space
{
    [DefaultValue("m")]Margin,
    [DefaultValue("p")]Padding
}