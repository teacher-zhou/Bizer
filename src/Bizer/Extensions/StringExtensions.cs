using System.Text;

namespace Bizer;

/// <summary>
/// 字符串扩展类。
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 判断当前字符串不是 <c>null</c> 还是空字符串。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <returns>若不为 <c>null</c> 或空字符串则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsNotNullOrEmpty(this string value)
        => !string.IsNullOrEmpty(value);

    /// <summary>
    /// 判断当前字符串不是 <c>null</c> 、空字符串或空白字符串。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <returns>若不为 <c>null</c> 、空字符串或空白字符串则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsNotNullOrWhiteSpace(this string value)
        => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 使用指定数组的参数值替换当前字符串的格式化占位字符。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <param name="args">要替换格式化占位的值。</param>
    /// <returns>替换后的字符串。</returns>
    public static string StringFormat(this string value, params object[] args)
        => string.Format(value, args);

    /// <summary>
    /// 使用指定的分隔符，将当前数组对象进行连接。
    /// </summary>
    /// <param name="values">要连接的对象数组。</param>
    /// <param name="seperator">分隔符字符串。</param>
    /// <returns>数组使用分隔符进行连接过后的字符串。</returns>
    public static string JoinAsString<T>(this IEnumerable<T> values, string seperator)
        => string.Join(seperator, values);

    /// <summary>
    /// 从当前字符串获取字节数组。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <param name="encoding">字符编码。<c>null</c> 使用 <see cref="Encoding.UTF8"/> 编码字符。</param>
    /// <returns>字节数组。</returns>
    public static byte[] GetBytes(this string value, Encoding? encoding = default)
    {
        Encoding code = encoding ?? Encoding.UTF8;
        return code.GetBytes(value);
    }

    /// <summary>
    /// 从当前字节数组获取字符串。
    /// </summary>
    /// <param name="bytes">当前字节数组。</param>
    /// <param name="encoding">字符编码。<c>null</c> 使用 <see cref="Encoding.UTF8"/> 编码字符。</param>
    /// <returns>符合字节数组的字符串。</returns>
    public static string GetString(this byte[] bytes, Encoding? encoding = default)
    {
        Encoding code = encoding ?? Encoding.UTF8;
        return code.GetString(bytes);
    }
    /// <summary>
    /// 将当前字符串转换为用 base-64 数字编码的等效字符串表示形式。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <param name="encoding">字符编码。<c>null</c> 使用 <see cref="Encoding.UTF8"/> 编码字符。</param>
    /// <param name="options"><see cref="Base64FormattingOptions.InsertLineBreaks"/> 表示每76个字符插入一个换行符，或者 <see cref="Base64FormattingOptions.None"/> 表示不插入换行符。</param>
    /// <returns>以64进制表示的字符串。</returns>
    public static string ToBase64String(this string value, Encoding? encoding = default, Base64FormattingOptions options = Base64FormattingOptions.None)
        => Convert.ToBase64String(value.GetBytes(encoding), options);

    /// <summary>
    /// 将指定的字符串(将二进制数据编码为base-64数字)转换为等效的数组。
    /// </summary>
    /// <param name="value">当前 Base-64 数据编码的字符串。</param>
    /// <param name="encoding">字符编码。<c>null</c> 使用 <see cref="Encoding.UTF8"/> 编码字符。</param>
    /// <returns>等效与 Base-64 字符串的原始字符。</returns>
    public static string FromBase64String(this string value, Encoding? encoding = default)
        => Convert.FromBase64String(value).GetString(encoding);
}
