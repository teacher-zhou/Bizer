using ComponentBuilder.JSInterop;

using Microsoft.JSInterop;

namespace Bizer.AspNetCore.Components;

public static class JSInteropExtensions
{
    public static ValueTask<IJSModule> ImportBizerComponentAsync(this IJSRuntime js)
        => js.ImportAsync("./_content/Bizer.AspNetCore.Components/bizer.js");
}
