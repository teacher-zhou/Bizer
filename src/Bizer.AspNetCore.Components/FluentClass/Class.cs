namespace Bizer.AspNetCore.Components;
public static class Class
{
    public static IContainerFluentBreakPoint Container => new ContainerFluentClassProvider();

    public static INavbarFluentBreakPoint Navbar => new NavbarFluentClassProvider();

    public static ISpacingFluentClass Margin => new MarginFluentClassProvider();

    public static ISpacingFluentClass Padding => new PaddingFluentClassProvider();

    public static IGridRowColumnSize RowColumn => new GridRowColumnFluentClassProvider();

    public static IGridColumnSize Column => new GridColumnFluentClassProvider();
    public static IOffsetSize Offset => new OffsetFluentClassProvider();

    public static IGutterSideWithBreakPoint Gutter =>new GutterFluentClassProvider();
}
