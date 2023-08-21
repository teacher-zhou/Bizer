namespace Bizer.AspNetCore.Components;

/// <summary>
/// 对话框默认实现。
/// </summary>
class DialogService : IDialogService
{

    /// <inheritdoc/>
    public event Action<Guid,DialogConfiguration, DialogParameters>? OnOpening;

    /// <inheritdoc/>
    public event Action<Guid, DialogResult>? OnClosing;

    private DialogReference _reference;

    /// <inheritdoc/>
    public Task Close(Guid id, DialogResult result)
    {
        _reference.SetResult(result);
        OnClosing?.Invoke(id, result);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IDialogReference> Open<TDialogTemplate>(DialogConfiguration? configuration=default, DialogParameters? parameters = default) where TDialogTemplate : IComponent
    {
        configuration ??= new();

        parameters ??= new();
        parameters.SetDialogTemplate<TDialogTemplate>();
        return Open(configuration, parameters);
    }

    Task<IDialogReference> Open(DialogConfiguration configuration, DialogParameters parameters)
    {
        if ( parameters is null )
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var reference = new DialogReference();
        _reference = reference;
        OnOpening?.Invoke(_reference.Id,configuration, parameters);
        return Task.FromResult((IDialogReference)reference);
    }
}
