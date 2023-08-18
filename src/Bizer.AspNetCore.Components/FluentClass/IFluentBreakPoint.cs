namespace Bizer.AspNetCore.Components;

/// <summary>
/// 提供响应式断点的功能。
/// </summary>
/// <typeparam name="TFluentProvider">提供器类型。</typeparam>
public interface IFluentBreakPoint<out TFluentProvider> : IFluentClassProvider
{
    /// <summary>
    /// 表示小型设备的尺寸，一般用于横屏手机，约576px以上。
    /// <para>
    /// class 为 sm。
    /// </para>
    /// </summary>
    TFluentProvider OnSM { get; }
    /// <summary>
    /// 表示中型设备的尺寸，一般用于平板电脑，约768px以上。
    /// <para>
    /// class 为 md。
    /// </para>
    /// </summary>
    TFluentProvider OnMD { get; }
    /// <summary>
    /// 表示大型设备的尺寸，一般用于台式电脑显示器，约992px以上。
    /// <para>
    /// class 为 lg。
    /// </para>
    /// </summary>
    TFluentProvider OnLG { get; }
    /// <summary>
    /// 表示超大型设备的尺寸，一般用于宽屏显示器，约1200px以上。
    /// <para>
    /// class 为 xl。
    /// </para>
    /// </summary>
    TFluentProvider OnXL { get; }
    /// <summary>
    /// 表示超级大型设备的尺寸，一般用于大屏显示器，约1400px以上。
    /// </summary>
    TFluentProvider OnXXL { get; }
}
