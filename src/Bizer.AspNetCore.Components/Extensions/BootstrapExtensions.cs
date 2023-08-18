namespace Bizer.AspNetCore.Components;

public static class BootstrapExtensions
{
    /// <summary>
    /// 是否为深色背景。
    /// </summary>
    /// <param name="color">背景颜色。</param>
    /// <returns>如果是深色，返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsDeepBgColor(this BgColor? color)
    {
        if (color == null)
        {
            return false;
        }
        return new[] { BgColor.Primary, BgColor.Secondary, BgColor.Success, BgColor.Danger, BgColor.Dark }.Contains(color.Value);
    }

    /// <summary>
    /// 是否为深色通用颜色。
    /// </summary>
    /// <param name="color">通用颜色。</param>
    /// <returns>如果是深色，返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsDeepColor(this Color? color)
    {
        if (color == null)
        {
            return false;
        }
        return new[] { Color.Primary, Color.Secondary, Color.Success, Color.Danger, Color.Dark }.Contains(color.Value);
    }

    /// <summary>
    /// 获取适合的通用颜色的 CSS。
    /// <para>
    /// 若是深色，将使用浅色文字；否则使用深色文字。
    /// </para>
    /// </summary>
    /// <param name="color">通用颜色。</param>
    /// <returns>通用颜色和适合的文字颜色组合的 CSS 字符串。</returns>
    public static string GetSuitableColorClass(this Color? color, string prefix)
    {
        if (color.IsDeepColor())
        {
            return $"{color?.GetCssClass(prefix)} {TextColor.Light.GetCssClass()}";
        }
        return $"{color?.GetCssClass(prefix)} {TextColor.Dark.GetCssClass()}";
    }

    /// <summary>
    /// 获取适合的背景颜色的 CSS。
    /// <para>
    /// 若是深色背景，将使用浅色文字；否则使用深色文字。
    /// </para>
    /// </summary>
    /// <param name="color">背景颜色。</param>
    /// <param name="gradient">是否使用渐变色。</param>
    /// <returns>背景颜色和适合的文字颜色组合的 CSS 字符串。</returns>
    public static string GetSuitableBgColorClass(this BgColor? color, bool gradient = default)
    {
        var gradientClass = gradient ? " bg-gradient" : string.Empty;
        if (color.IsDeepBgColor())
        {
            return $"{color?.GetCssClass()}{gradientClass} {TextColor.Light.GetCssClass()}";
        }
        return $"{color?.GetCssClass()}{gradientClass} {TextColor.Dark.GetCssClass()}";
    }

    /// <summary>
    /// 将对应的背景颜色转换成通用颜色。
    /// </summary>
    /// <param name="color">要转换的背景颜色。</param>
    /// <returns>对应的 <see cref="Color"/> 枚举。</returns>
    /// <exception cref="InvalidCastException">没有对应的转换。</exception>
    public static Color? ToColor(this BgColor? color)
        => color switch
        {
            BgColor.Primary => Color.Primary,
            BgColor.Secondary => Color.Secondary,
            BgColor.Success => Color.Success,
            BgColor.Info => Color.Info,
            BgColor.Warning => Color.Warning,
            BgColor.Danger => Color.Danger,
            BgColor.Dark => Color.Dark,
            BgColor.Light => Color.Light,
            null => default,
            _ => throw new InvalidCastException("没有对应的转换")
        };
    /// <summary>
    /// 将通用颜色转换成背景颜色。
    /// </summary>
    /// <param name="color">要转换的通用颜色。</param>
    /// <returns>对应的 <see cref="BgColor"/> 枚举。</returns>
    /// <exception cref="InvalidCastException">没有对应的转换。</exception>
    public static BgColor? ToBgColor(this Color? color)
        => color switch
        {
            Color.Primary => BgColor.Primary,
            Color.Secondary => BgColor.Secondary,
            Color.Success => BgColor.Success,
            Color.Info => BgColor.Info,
            Color.Warning => BgColor.Warning,
            Color.Danger => BgColor.Danger,
            Color.Dark => BgColor.Dark,
            Color.Light => BgColor.Light,
            null => default,
            _ => throw new InvalidCastException("没有对应的转换")
        };
}
