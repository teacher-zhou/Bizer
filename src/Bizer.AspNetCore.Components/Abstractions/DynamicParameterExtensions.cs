namespace Bizer.AspNetCore.Components.Abstractions;

internal static class DynamicParameterExtensions
{
    public static void SetDynamicTemplate<TTemplate>(this DynamicParameters parameters)
    {
        var type = typeof(TTemplate);
        parameters.Set("DynamicTemplate", type);
    }

    public static Type GetDynamicTemplate(this DynamicParameters parameters)
        => parameters.Get("DynamicTemplate") as Type ?? throw new InvalidCastException("DynamicTemplate casting error");

    static void SetFragment(this DynamicParameters parameters, string name, RenderFragment? fragment)
        => parameters.Set(name, fragment);
    static RenderFragment? GetFragment(this DynamicParameters parameters, string name)
        => parameters.Get<RenderFragment>(name);

    public static void SetTitle(this DynamicParameters parameters, RenderFragment? fragment) => parameters.SetFragment("Title", fragment);
    public static RenderFragment? GetTitle(this DynamicParameters parameters) => parameters.GetFragment("Title");

    public static void SetContent(this DynamicParameters parameters, RenderFragment? fragment) => parameters.SetFragment("Content", fragment);
    public static RenderFragment? GetContent(this DynamicParameters parameters) => parameters.GetFragment("Content");
}
