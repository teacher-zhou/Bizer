
using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 用于动态渲染 <see cref="Dialog"/> 组件的容器。
/// </summary>
public class DialogContainer:ComponentBase,IDisposable
{
    [Parameter]public RenderFragment? ChildContent { get; set; }
    [Inject]IDialogService DialogService { get; set; }

    Dictionary<Guid,(DialogConfiguration? configuration, DynamicParameters? parameters)> DialogCollection = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogService.OnOpening += DialogService_OnOpening;
        DialogService.OnClosing += DialogService_OnClosing;
    }

    private void DialogService_OnClosing(Guid id, DialogResult result)
    {
        Close(id);
    }

    private void DialogService_OnOpening(Guid id,DialogConfiguration? configuration, DynamicParameters? parameters)
    {
        //Thread.Sleep(400);
        DialogCollection.Add(id, new(configuration,parameters));
        InvokeAsync(StateHasChanged);
    }


    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        foreach (var item in DialogCollection)
        {
            builder.CreateCascadingComponent(this, 0, content =>
            {
                content.CreateCascadingComponent(item.Value.parameters, 0, inner =>
                {
                    inner.Component<DialogRenderer>()
                    .Attribute(m=>m.Id,item.Key)
                    .Attribute(m => m.Configuration, item.Value.configuration)
                    .Key(item.Key)
                    .Close();
                });

            });
        }
    }
    
    internal void Close(Guid id)
    {
        Thread.Sleep(150);//延迟等动画效果完成
        if (DialogCollection.Remove(id))
        {
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        DialogService.OnOpening -= DialogService_OnOpening;
        DialogService.OnClosing -= DialogService_OnClosing;
    }
}
