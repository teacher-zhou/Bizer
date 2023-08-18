namespace Bizer.AspNetCore.Components;

/// <summary>
/// 提供 Fluent Class 的功能。
/// </summary>
public interface IFluentClass
{
    ISpacingFluentClass Margin { get; }
    ISpacingFluentClass Padding { get; }
}

internal class FluentClass : IFluentClass
{
    public ISpacingFluentClass Margin => new MarginFluentClassProvider();
    public ISpacingFluentClass Padding => new PaddingFluentClassProvider();
}
public interface IFluentAndClass
{
    IFluentClass And => new FluentClass();
}
public interface IFluentAndClass<TAndClass>
{
    TAndClass And { get; }
}
