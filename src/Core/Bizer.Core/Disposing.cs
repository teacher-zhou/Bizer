namespace Bizer;

/// <summary>
/// 表示一个需要进行释放的执行方法。
/// </summary>
public class Disposing : IDisposable
{
    private readonly Action _disposeAction;

    /// <summary>
    /// 初始化 <see cref="Disposing"/> 类的新实例。
    /// </summary>
    /// <param name="disposeAction">要释放的执行。</param>
    private Disposing(Action disposeAction)
    {
        _disposeAction = disposeAction;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose() => _disposeAction?.Invoke();

    /// <summary>
    /// 在释放时资源时需要执行的行为。
    /// </summary>
    /// <param name="disposeAction">释放时执行的委托</param>
    /// <returns></returns>
    public static IDisposable Perform(Action disposeAction) => new Disposing(disposeAction);
}
