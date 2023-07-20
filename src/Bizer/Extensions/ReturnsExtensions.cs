using Microsoft.Extensions.Logging;

namespace Bizer;
public static class ReturnsExtensions
{
    static Returns LogAsReturns(this Returns instance, ILogger? logger,LogLevel level,Exception? exception=default)
    {
        logger?.Log(level, exception, instance.Messages.JoinAsString("；"));
        return instance;
    }
    static Returns<TResult> LogAsReturns<TResult>(this Returns<TResult> instance, ILogger? logger, LogLevel level, Exception? exception = default)
    {
        logger?.Log(level, exception, instance.Messages.JoinAsString("；"));
        return instance;
    }

    public static Returns Failed(this Returns instance, IEnumerable<string> errors) => instance.Failed(errors.ToArray());
    public static Returns<TResult> Failed<TResult>(this Returns<TResult> instance, IEnumerable<string> errors) => instance.Failed(errors.ToArray());

    public static Returns LogTrace(this Returns instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Trace);
    public static Returns LogDebug(this Returns instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Debug);
    public static Returns LogInfo(this Returns instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Information);
    public static Returns LogWarning(this Returns instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Warning);
    public static Returns LogError(this Returns instance, ILogger? logger, Exception? exception = default) => instance.LogAsReturns(logger, LogLevel.Error, exception);
    public static Returns LogFatal(this Returns instance, ILogger? logger, Exception? exception = default) => instance.LogAsReturns(logger, LogLevel.Critical, exception);
    public static Returns<TResult> LogTrace<TResult>(this Returns<TResult> instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Trace);
    public static Returns<TResult> LogDebug<TResult>(this Returns<TResult> instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Debug);
    public static Returns<TResult> LogInfo<TResult>( this Returns<TResult> instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Information);
    public static Returns<TResult> LogWarning<TResult>(this Returns<TResult> instance, ILogger? logger) => instance.LogAsReturns(logger, LogLevel.Warning);
    public static Returns<TResult> LogError<TResult>(this Returns<TResult> instance, ILogger? logger, Exception? exception = default) => instance.LogAsReturns(logger, LogLevel.Error, exception);
    public static Returns<TResult> LogFatal<TResult>(this Returns<TResult> instance, ILogger? logger, Exception? exception = default) => instance.LogAsReturns(logger, LogLevel.Critical, exception);
}
