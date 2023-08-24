using System.ComponentModel;
using System.Text;

namespace Bizer.AspNetCore.Components;
public interface ISpacingBreakPoint:IFluentBreakPoint<ISpacingSize>
{
}

public interface ISpacingSideWithBreakPoint : ISpacingSize, ISpacingBreakPoint
{

}

public interface ISpacingSides: ISpacingSideWithBreakPoint
{
    ISpacingSideWithBreakPoint FromTop { get; }
    ISpacingSideWithBreakPoint FromBottom { get; }
    ISpacingSideWithBreakPoint FromStart { get; }
    ISpacingSideWithBreakPoint FromEnd { get; }
    ISpacingSideWithBreakPoint FromX { get; }
    ISpacingSideWithBreakPoint FromY { get; }

    ISpacingSideWithBreakPoint FromAll { get; }
}

public interface ISpacingSize:IFluentClassProvider
{
    ISpacingSides Auto { get; }
    ISpacingSides None { get; }
    ISpacingSides Is1 { get; }
    ISpacingSides Is2 { get; }
    ISpacingSides Is3 { get; }
    ISpacingSides Is4 { get; }
    ISpacingSides Is5 { get; }
}

public interface ISpacingFluentClass: ISpacingSize, ISpacingSides, ISpacingSideWithBreakPoint,IFluentClassProvider
{

}

internal abstract class SpacingFluentClassProvider : FluentClassProvider<SpacingType, SpaceDefinition>, ISpacingFluentClass
{
    private readonly SpacingType _space;
    private SpaceDefinition _spaceDefinition = new();

    protected SpacingFluentClassProvider(SpacingType space)
    {
        _space = space;
    }

    public ISpacingSize OnSM => WithSize(BreakPoint.SM);
    public ISpacingSize OnMD => WithSize(BreakPoint.MD);
    public ISpacingSize OnLG => WithSize(BreakPoint.LG);
    public ISpacingSize OnXL => WithSize(BreakPoint.XL);
    public ISpacingSize OnXXL => WithSize(BreakPoint.XXL);
    public ISpacingSideWithBreakPoint FromTop => WithBreakPoint("t");
    public ISpacingSideWithBreakPoint FromBottom => WithBreakPoint("b");
    public ISpacingSideWithBreakPoint FromStart => WithBreakPoint("s");
    public ISpacingSideWithBreakPoint FromEnd => WithBreakPoint("e");
    public ISpacingSideWithBreakPoint FromX => WithBreakPoint("x");
    public ISpacingSideWithBreakPoint FromY => WithBreakPoint("y");
    public ISpacingSideWithBreakPoint FromAll => WithBreakPoint(default);
    public ISpacingSides Auto => WithSide(default);
    public ISpacingSides None => WithSide( Space.None);
    public ISpacingSides Is1 => WithSide( Space.Is1);
    public ISpacingSides Is2 => WithSide(Space.Is2);
    public ISpacingSides Is3 => WithSide(Space.Is3);
    public ISpacingSides Is4 => WithSide(Space.Is4);
    public ISpacingSides Is5 => WithSide(Space.Is5);


    ISpacingSides WithSide(Space? space)
    {
        if ( space.HasValue )
        {
            _spaceDefinition.Size = space.GetCssClass();
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

    ISpacingSideWithBreakPoint WithBreakPoint(string? side)
    {
        _spaceDefinition.Side = side;
        return this;
    }
    protected override string? Format(SpacingType key)
        => Format(key, _spaceDefinition);

    protected override string? Format(SpacingType key, SpaceDefinition value)
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
    public MarginFluentClassProvider() : base(SpacingType.Margin)
    {
    }
}
internal class PaddingFluentClassProvider : SpacingFluentClassProvider, ISpacingFluentClass
{
    public PaddingFluentClassProvider() : base(SpacingType.Padding)
    {
    }
}

internal struct SpaceDefinition
{
    public string Size;
    public string? Side;
    public BreakPoint? BreakPoint;
}

internal enum SpacingType
{
    [DefaultValue("m")]Margin,
    [DefaultValue("p")]Padding
}