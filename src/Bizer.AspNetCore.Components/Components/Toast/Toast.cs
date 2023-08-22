namespace Bizer.AspNetCore.Components;

public class Toast : BizerChildConentComponentBase
{
    [CascadingParameter]ToastRenderer Renderer { get; set; }
    [Parameter]public RenderFragment? HeaderContent { get; set; }

    [Parameter]public bool Closable { get; set; }


    bool _hasInitialized;
    protected override void OnInitialized()
    {
        if (!_hasInitialized)
        {
            Renderer?.Set(this);
            _hasInitialized = true;
        }
    }
}
