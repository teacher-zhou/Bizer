using System.Text.Json.Serialization;

using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

/// <summary>
/// 配置。
/// </summary>
public class ToastConfiguration
{
    /// <summary>
    /// 显示的位置。
    /// </summary>
    [JsonIgnore]
    public Placement Placement { get; set; } = Placement.TopRight;
    /// <summary>
    /// 显示延迟时间，默认 5000 毫秒。
    /// </summary>
    public int? Delay { get; set; } = 5000;

    internal DynamicParameters Parameters { get; set; }
}
