namespace Bizer.AspNetCore.Components;

public class ToastContainer : BlazorComponentBase
{
    [Inject]IToastService ToastService { get; set; }

    protected Dictionary<Placement, List<ToastConfiguration>> Toasters = new();

    Func<ToastConfiguration,Task>? _closeToastHandler;

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["aria-live"] = "polite";
        attributes["aria-atomic"] = "true";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        foreach (var item in Toasters)
        {
            builder.Div("taost-container")
                .Class("position-fixed")
                .Class("m-3")
                .Class(item.Key.GetCssClass())
                .Content(content =>
                {
                    foreach (var toastConfiguration in item.Value)
                    {
                        content.Component<Toast>()
                                .Attribute(m => m.Configuration, toastConfiguration)
                                .Attribute(m => m.CloseHandler, _closeToastHandler)
                                .Close();
                    }
                })
                .Close();
        }
    }
    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ToastService!.OnShowing += ToastService_OnShowing;
        _closeToastHandler = RemoveItem;
    }


    /// <summary>
    /// Notifies the message on showing.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A Task.</returns>
    private async Task ToastService_OnShowing(ToastConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        await AddItem(configuration);
        await this.Refresh();
    }

    /// <summary>
    /// Adds the item.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A Task.</returns>
    protected async Task AddItem(ToastConfiguration configuration)
    {
        var key = configuration.Placement;
        if (Toasters.ContainsKey(key))
        {
            Toasters[key].Add(configuration);
        }
        else
        {
            Toasters.Add(key, new() { configuration });
        }

        await this.Refresh();

        await OnTimeoutRemove();

        async Task OnTimeoutRemove()
        {
            using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(configuration.Delay ??= Timeout.Infinite));
            await timer.WaitForNextTickAsync();
            await RemoveItem(configuration);
        }
    }

    /// <summary>
    /// Removes the item.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A Task.</returns>
    protected Task RemoveItem(ToastConfiguration configuration)
    {
        var key = configuration.Placement;
        if (Toasters.ContainsKey(key))
        {
            Toasters[key].Remove(configuration);

            if (!Toasters[key].Any())
            {
                Toasters.Remove(key);
            }
        }
        return this.Refresh();
    }

    protected override void DisposeComponentResources()
    {
        if (ToastService is not null)
        {
            ToastService.OnShowing -= ToastService_OnShowing;
        }
    }
}
