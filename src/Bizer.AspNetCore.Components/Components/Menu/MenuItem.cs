namespace Bizer.AspNetCore.Components;

public class MenuItem
{
    public string? Title { get; set; }
    public object? Icon { get; set; }
    public string? Link { get; set; }
    public bool Divider { get; set; }
    public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

}
