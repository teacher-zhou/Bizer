namespace Bizer.AspNetCore.Components;

public abstract class ToastTemplateBase:ComponentBase
{
    /// <summary>
    /// 用于操作的对话框上下文。
    /// </summary>
    [CascadingParameter] protected ToastContext Context { get; set; }
}
