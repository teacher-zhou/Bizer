namespace Bizer;

/// <summary>
/// Task 任务扩展。
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// 转换成 <see cref="ValueTask"/> 类型。
    /// </summary>
    /// <param name="task">要转换的任务。</param>
    public static ValueTask ToValueTask(this Task task) => new(task);

    /// <summary>
    /// 转换成 <see cref="ValueTask{TResult}"/> 类型。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="task">要转换的任务。</param>
    public static ValueTask<TResult> ToValueTask<TResult>(this Task<TResult> task) => new(task);

    /// <summary>
    /// 转换成 <see cref="ValueTask{TResult}"/> 类型。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="result">要转换的结果。</param>
    public static ValueTask<TResult> ToValueTask<TResult>(this TResult result) => new(result);

    /// <summary>
    /// 将当前对象创建一个 <see cref="Task{TResult}"/> 的任务。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="result">当前结果。</param>
    /// <returns></returns>
    public static Task<TResult> ToResultTask<TResult>(this TResult result) => Task.FromResult(result);
    /// <summary>
    /// 将当前异常创建具备异常的任务。
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static Task ToExceptionTask(this Exception exception) => Task.FromException(exception);
    /// <summary>
    /// 创建取消任务。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task ToCancellationTask(this CancellationToken cancellationToken) => Task.FromCanceled(cancellationToken);
}
