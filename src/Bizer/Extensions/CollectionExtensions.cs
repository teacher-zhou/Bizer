namespace Bizer;

/// <summary>
/// 集合扩展。
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 对当前集合进行遍历操作。
    /// </summary>
    /// <typeparam name="T">数据源类型。</typeparam>
    /// <param name="source">数据源。</param>
    /// <param name="action">遍历循环的行为委托。</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
}
