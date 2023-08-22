using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public static class ToastDynamicParameterExtensions
{
    internal static void SetCloseHandler(this DynamicParameters parameters, Func<ToastConfiguration, Task> handler)
        => parameters.Set("CloseToastHandler", handler);

    public static Func<ToastConfiguration, Task>? GetCloseToastHandler(this DynamicParameters parameters)
        => parameters.Get<Func<ToastConfiguration, Task>>("CloseToastHandler");

}
