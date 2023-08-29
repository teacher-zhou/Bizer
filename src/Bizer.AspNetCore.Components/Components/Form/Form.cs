using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 验证表单。
/// </summary>
[ParentComponent]
[HtmlTag("form")]
[CssClass("row")]
public class Form : BizerComponentBase
{
    private bool _hasSetEditContextExplicitly;
    /// <summary>
    /// 要验证的表单模型。
    /// </summary>
    [Parameter]public object? Model { get; set; }
    /// <summary>
    /// 用于自定义验证的上下文。
    /// </summary>
    [Parameter]
    public EditContext? EditContext
    {
        get => _fixedEditContext;
        set
        {
            _fixedEditContext = value;
            _hasSetEditContextExplicitly = value != null;
        }
    }
    /// <summary>
    /// 一个表达提交的回调。
    /// </summary>
    [Parameter]public EventCallback<bool> OnSubmit { get; set; }
    /// <summary>
    /// 表单的任意内容。
    /// </summary>
    [Parameter] public RenderFragment<EditContext?>? ChildContent { get; set; }

    /// <summary>
    /// 让 label 变成浮动样式。
    /// </summary>
    [Parameter][CssClass("form-floating")]public bool Floating { get; set; }
    /// <summary>
    /// 禁用验证。
    /// </summary>
    [Parameter]public bool DisableValidation { get; set; }

    /// <summary>
    /// 当表单验证错误时的任意内容，并传入一个验证错误的参数。
    /// </summary>
    [Parameter]public RenderFragment<IEnumerable<string?>>? ValidationErrorContent { get; set; }

    /// <summary>
    /// 使用行内自动水平布局，并根据设置的列数自动水平布局，超过则自动换行。 
    /// </summary>
    [Parameter][CssClass("row-cols-md-auto")] public bool AutoInline { get; set; }

    /// <summary>
    /// <see cref="FormRow"/> 的行间距。
    /// </summary>
    [Parameter][CssClass]public Gap? RowSpace { get; set; }

    /// <summary>
    /// 获取一个布尔值，表示表单是否正在提交状态。
    /// </summary>
    public bool IsSubmitting { get; private set; }

    /// <summary>
    /// 获取一个布尔值，表示表单是否已经合法通过验证。
    /// </summary>
    public bool Valid { get;private set; }

    EditContext? _fixedEditContext;
    IEnumerable<string?> _errors = Enumerable.Empty<string?>();



    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_hasSetEditContextExplicitly && Model is not null)
        {
            throw new InvalidOperationException($"参数 {nameof(Model)} 和 {nameof(EditContext)} 必须选择一个提供，但不能都同时提供");
        }

        else if (!_hasSetEditContextExplicitly && Model is null)
        {
            throw new InvalidOperationException($"参数 {nameof(Model)} 和 {nameof(EditContext)} 必须至少提供一个");
        }


        if (Model != null && Model != _fixedEditContext?.Model)
        {
            _fixedEditContext = new EditContext(Model!);
        }

        _fixedEditContext!.SetFieldCssClassProvider(new BootstrapFieldCssClassProvider());
    }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.CreateCascadingComponent(_fixedEditContext, sequence, content =>
        {
            if (!DisableValidation)
            {
                content.CreateComponent<DataAnnotationsValidator>(0);
            }

            content.AddContent(1, ValidationErrorContent, _errors);

            content.AddContent(2, ChildContent, _fixedEditContext);

        }, isFixed: true);
    }

    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["onsubmit"] = Submit;
    }

    /// <summary>
    /// 执行提交操作。
    /// </summary>
    /// <exception cref="ArgumentNullException"><see cref="EditContext"/> 是 null。</exception>
    public async Task Submit()
    {
        if(_fixedEditContext is null)
        {
            throw new ArgumentNullException(nameof(_fixedEditContext));
        }
        if (DisableValidation)
        {
            return;
        }

        Valid = _fixedEditContext.Validate();

        if (!Valid)
        {
            _errors = _fixedEditContext.GetValidationMessages();
            return;
        }

        IsSubmitting = true;
        StateHasChanged();

        await Task.Delay(1000);
        await OnSubmit.InvokeAsync(Valid);

        IsSubmitting = false;
        StateHasChanged();
    }
}
