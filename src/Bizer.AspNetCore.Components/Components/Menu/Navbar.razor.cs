namespace Bizer.AspNetCore.Components;

[CssClass("navbar")]
public partial class Navbar
{
    public Navbar()
    {

    }

    protected override void BuildCssClass(ICssClassBuilder builder)
    {
        builder.Append(Options.Theme.MenuColor.GetCssClass("bg-"))
            .Append("navbar-dark", Options.Theme.MenuColor.IsDark());

        builder.Append("navbar-expand-lg");
    }
}
