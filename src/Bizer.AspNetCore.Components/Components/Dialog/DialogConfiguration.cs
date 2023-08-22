using System.Text.Json.Serialization;

namespace Bizer.AspNetCore.Components;

public class DialogConfiguration
{
    /// <summary>
    /// 内容超长时可进行滚动。
    /// </summary>
    [JsonIgnore]
    public bool Scrollable { get; set; }
    /// <summary>
    /// 对话框屏幕居中显示。
    /// </summary>
    [JsonIgnore]
    public bool Centered { get; set; }
    /// <summary>
    /// 禁用背景交互。
    /// </summary>
    public bool Backdrop { get; set; } = true;
    /// <summary>
    /// 焦点到对话框。
    /// </summary>
    public bool Focus { get; set; } = true;
    /// <summary>
    /// 按 ESC 时可关闭对话框。
    /// </summary>
    public bool Keyboard { get; set; } = true;

    [JsonIgnore]
    public Func<Task>? OnOpening { get; set; }
    [JsonIgnore]
    public Func<Task>? OnOpened { get; set; }
    [JsonIgnore]
    public Func<Task>? OnClosing { get; set; }
    [JsonIgnore]
    public Func<Task>? OnClosed { get; set; }
}
