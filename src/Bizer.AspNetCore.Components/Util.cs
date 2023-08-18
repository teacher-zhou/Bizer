namespace Bizer.AspNetCore.Components;

public static class Util
{
    public static bool IsDark(this Color color)
    {
        return new[] { Color.Dark, Color.Primary, Color.Danger, Color.Info, Color.Warning, Color.Success, Color.Secondary }.Contains(color);
    }

    public static bool IsDark(this BgColor color)
    {
        return new[] { BgColor.Dark, BgColor.Primary, BgColor.Danger, BgColor.Info, BgColor.Warning, BgColor.Success, BgColor.Secondary }.Contains(color);
    }
}
