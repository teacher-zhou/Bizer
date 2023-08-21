namespace Bizer.AspNetCore.Components;

public class ToastConfiguration
{
    public Placement Placement { get; set; } = Placement.TopRight;
    public int? Delay { get; set; } = 5000;

    public Color? Color { get; set; }

    public bool Closable { get; set; } = true;

    public RenderFragment? HeaderTemplate { get; set; }
    public string? Title { get; set; }

    public RenderFragment? BodyTemplate { get; set; }
    public string? Body { get; set; }


    internal RenderFragment GetBody() => BodyTemplate ??= builder => builder.AddContent(0, Body);
}
