namespace Bizer.AspNetCore.Components;
public static class Class
{
    public static IContainerFluentBreakPoint Container => new ContainerFluentClassProvider();

    public static INavbarFluentBreakPoint Navbar => new NavbarFluentClassProvider();

    public static ISpacingSize Margin => new MarginFluentClassProvider();
}


