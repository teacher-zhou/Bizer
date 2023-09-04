using System.Diagnostics.CodeAnalysis;

namespace Bizer;

/// <summary>
/// 对象检查器。
/// </summary>
public static class Checker
{
    /// <summary>
    /// 指定的值不能是 <c>null</c>，否则抛出 <see cref="ArgumentNullException"/> 异常。
    /// </summary>
    /// <typeparam name="T">值的类型。</typeparam>
    /// <param name="value">要检查的值。</param>
    /// <param name="paramName">参数名称。</param>
    /// <returns>不为 null 的值。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> 是 <c>null</c>。</exception>
    public static T NotNull<T>(T value, [NotNull] string paramName)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
        return value;
    }

    /// <summary>
    /// 指定的值不能是 <c>null</c>，否则抛出 <see cref="ArgumentNullException"/> 异常。
    /// </summary>
    /// <typeparam name="T">值的类型。</typeparam>
    /// <param name="value">要检查的值。</param>
    /// <param name="paramName">参数名称。</param>
    /// <param name="message">自定义错误消息。</param>
    /// <returns>不为 null 的值。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> 是 <c>null</c>。</exception>
    public static T NotNull<T>(T value, [NotNull] string paramName, [NotNull] string message)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, message);
        }
        return value;
    }
    /// <summary>
    /// 检查指定字符串的值是否为 null 或空字符串，并抛出异常。
    /// </summary>
    /// <param name="value">要检查的字符串。</param>
    /// <param name="paramName">参数名称。</param>
    /// <returns>不为 null 或空字符串的字符串。</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> 是 null 或空字符串。</exception>
    public static string NotNullOrEmpty(string? value, [NotNull] string paramName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"'{paramName}' cannot be null or empty.", paramName);
        }
        return value;
    }

    /// <summary>
    /// 检查指定字符串的值是否为 null 或空字符串，并抛出异常。
    /// </summary>
    /// <param name="value">要检查的字符串。</param>
    /// <param name="paramName">参数名称。</param>
    /// <param name="message">自定义异常消息。</param>
    /// <returns>不为 null 或空字符串的字符串。</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> 是 null 或空字符串。</exception>
    public static string NotNullOrEmpty(string? value, [NotNull] string paramName, [NotNull] string message)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(message, paramName);
        }
        return value;
    }

    /// <summary>
    /// 检查指定字符串的值是否为 null 或空白字符串，并抛出异常。
    /// </summary>
    /// <param name="value">要检查的字符串。</param>
    /// <param name="paramName">参数名称。</param>
    /// <returns>不为 null 或空白字符串的字符串。</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> 是 null 或空白字符串。</exception>
    public static string NotNullOrWhiteSpace(string? value, [NotNull] string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
        }
        return value;
    }

    /// <summary>
    /// 检查指定字符串的值是否为 null 或空白字符串，并抛出异常。
    /// </summary>
    /// <param name="value">要检查的字符串。</param>
    /// <param name="paramName">参数名称。</param>
    /// <param name="message">自定义异常消息。</param>
    /// <returns>不为 null 或空白字符串的字符串。</returns>
    /// <exception cref="ArgumentException"><paramref name="value"/> 是 null 或空白字符串。</exception>
    public static string NotNullOrWhiteSpace(string? value, string paramName, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, paramName);
        }
        return value;
    }
}
