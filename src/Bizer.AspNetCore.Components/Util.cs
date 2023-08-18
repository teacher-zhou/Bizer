using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizer.AspNetCore.Components;

public static class Util
{
    public static bool IsDark(this Color color)
    {
        return new[] { Color.Dark, Color.Primary, Color.Danger, Color.Info, Color.Warning, Color.Success, Color.Secondary }.Contains(color);
    }

    public static bool IsDark(this Background color)
    {
        return new[] { Background.Dark, Background.Primary, Background.Danger, Background.Info, Background.Warning, Background.Success, Background.Secondary }.Contains(color);
    }
}
