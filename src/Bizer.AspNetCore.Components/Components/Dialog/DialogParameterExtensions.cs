namespace Bizer.AspNetCore.Components;

internal static class DialogParameterExtensions
{
    public static void SetDialogTemplate<TTemplate>(this DialogParameters parameters)
    {
        var type = typeof(TTemplate);
        parameters.Set("DialogTemplate", type);
    }

    public static Type GetDialogTemplate(this DialogParameters parameters) 
        => parameters.Get("DialogTemplate") as Type ?? throw new InvalidCastException("DialogTemplate casting error");

    static void SetFragment(this DialogParameters parameters,string name, RenderFragment? fragment) 
        => parameters.Set(name, fragment);
    static RenderFragment? GetFragment(this DialogParameters parameters,string name) 
        => parameters.Get<RenderFragment>(name);

    public static void SetTitle(this DialogParameters parameters, RenderFragment? fragment) => parameters.SetFragment("Title", fragment);
    public static RenderFragment? GetTitle(this DialogParameters parameters) => parameters.GetFragment("Title");

    public static void SetContent(this DialogParameters parameters, RenderFragment? fragment) => parameters.SetFragment("Content", fragment);
    public static RenderFragment? GetContent(this DialogParameters parameters) => parameters.GetFragment("Content");
}
