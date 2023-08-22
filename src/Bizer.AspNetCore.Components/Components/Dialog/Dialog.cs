namespace Bizer.AspNetCore.Components;
/// <summary>
/// 对话框。
/// </summary>
public class Dialog : ComponentBase
{
    /// <summary>
    /// 上下文。
    /// </summary>
    [CascadingParameter]DialogRenderer Modal { get; set; }
    /// <summary>
    /// 对话框消息的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? ChildContent { get; set; }
    /// <summary>
    /// 对话框标题的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? HeaderContent { get; set; }
    /// <summary>
    /// 对话框用于操作的任意内容。
    /// </summary>
    [Parameter]public RenderFragment? FooterContent { get; set; }

    [Parameter]public bool Closable { get; set; }


    bool _hasInitialized;
    protected override void OnInitialized()
    {
        if (!_hasInitialized)
        {
            Modal?.SetDialog(this);
            _hasInitialized = true;
        }
    }
}