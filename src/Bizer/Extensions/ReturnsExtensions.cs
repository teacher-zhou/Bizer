using Microsoft.Extensions.Logging;

namespace Bizer;
public static class ReturnsExtensions
{
    static Returns LogAsReturns(this Returns instance, ILogger? logger, LogLevel level,string? logMessage=default, Exception? exception = default)
    {
        logger?.Log(level, exception, message: logMessage ?? instance.Messages.JoinAsString("；"));
        return instance;
    }
    static Returns<TResult> LogAsReturns<TResult>(this Returns<TResult> instance, ILogger? logger, LogLevel level, string? logMessage = default, Exception? exception = default)
    {
        logger?.Log(level, exception, message: logMessage ?? instance.Messages.JoinAsString("；"));
        return instance;
    }

    /// <summary>
    /// 表示返回失败的结果。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="errors">错误消息的列表。</param>
    /// <returns></returns>
    public static Returns Failed(this Returns instance, IEnumerable<string> errors) => instance.Failed(errors.ToArray());
    /// <summary>
    /// 表示返回失败的结果。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="errors">错误消息的列表。</param>
    public static Returns<TResult> Failed<TResult>(this Returns<TResult> instance, IEnumerable<string> errors) 
        => instance.Failed(errors.ToArray());

    /// <summary>
    /// 记录日志是 TRACE 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    /// <returns></returns>
    public static Returns LogTrace(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Trace, logMessage, exception);

    /// <summary>
    /// 记录日志是 DEBUG 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    public static Returns LogDebug(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Debug, logMessage, exception);

    /// <summary>
    /// 记录日志是 INFO 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns LogInfo(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Information, logMessage, exception);

    /// <summary>
    /// 记录日志是 WARNING 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns LogWarning(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Warning, logMessage, exception);

    /// <summary>
    /// 记录日志是 ERROR 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns LogError(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Error, logMessage, exception);

    /// <summary>
    /// 记录日志是 FATAL 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns LogFatal(this Returns instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Critical, logMessage, exception);

    /// <summary>
    /// 记录日志是 TRACE 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogTrace<TResult>(this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Trace, logMessage, exception);

    /// <summary>
    /// 记录日志是 DEBUG 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogDebug<TResult>(this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Debug, logMessage, exception);

    /// <summary>
    /// 记录日志是 INFO 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogInfo<TResult>( this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Information, logMessage, exception);

    /// <summary>
    /// 记录日志是 WARNING 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogWarning<TResult>(this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default) 
        => instance.LogAsReturns(logger, LogLevel.Warning,logMessage, exception);

    /// <summary>
    /// 记录日志是 ERROR 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogError<TResult>(this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default)
        => instance.LogAsReturns(logger, LogLevel.Error, logMessage, exception);

    /// <summary>
    /// 记录日志是 FATAL 级别的日志。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="logger">日志对象。</param>
    /// <param name="logMessage">日志的消息。<c>null</c> 则使用 <see cref="Returns.Messages"/> 的值。</param>
    /// <param name="exception">异常对象。</param>
    public static Returns<TResult> LogFatal<TResult>(this Returns<TResult> instance, ILogger? logger, string? logMessage = default, Exception? exception = default)
        => instance.LogAsReturns(logger, LogLevel.Critical, logMessage, exception);

    /// <summary>
    /// 使返回结果具备指定的自定义代码。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="code">自定义代码。</param>
    /// <returns></returns>
    public static Returns WithCode(this Returns instance, string? code)
    {
        instance.Code = code;
        return instance;
    }

    /// <summary>
    /// 使返回结果具备指定的自定义代码。
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="code">自定义代码。</param>
    /// <returns></returns>
    public static Returns<TResult> WithCode<TResult>(this Returns<TResult> instance, string? code)
    {
        instance.Code = code;
        return instance;
    }
}
