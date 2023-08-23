using System.Text.Json.Serialization;

using Bizer.AspNetCore.Components.Abstractions;

namespace Bizer.AspNetCore.Components;

public class ToastConfiguration
{
    [JsonIgnore]
    public Placement Placement { get; set; } = Placement.TopRight;
    public int? Delay { get; set; } = 50000;

    internal DynamicParameters Parameters { get; set; }
}
