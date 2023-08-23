namespace Bizer.AspNetCore.Components;

/// <summary>
/// 消息提示组件，可自定义内容。
/// </summary>
public class Toast : ComponentBase
{
    [CascadingParameter]ToastRenderer Renderer { get; set; }
    /// <summary>
    /// 头部模板。
    /// </summary>
    [Parameter]public RenderFragment? HeaderContent { get; set; }
    /// <summary>
    /// 子内容。
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 是否显示关闭按钮。
    /// </summary>
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
