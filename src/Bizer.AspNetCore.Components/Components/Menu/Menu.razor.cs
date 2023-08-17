namespace Bizer.AspNetCore.Components;

partial class Menu
{
    [Inject]BizerComponentOptions Options { get; set; }
    [Parameter]public Func<Task<IEnumerable<MenuItem>>>? MenuItemsProvider { get; set; }


    IEnumerable<MenuItem> MenuItems { get; set; } = Enumerable.Empty<MenuItem>();

    ThemeContrast Theme => Options.Dark ? ThemeContrast.Dark : ThemeContrast.Light;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (MenuItemsProvider is not null)
            {
                MenuItems = await MenuItemsProvider.Invoke();
            }
        }
    }
}
