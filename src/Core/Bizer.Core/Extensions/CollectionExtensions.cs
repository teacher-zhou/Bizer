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

    public static void AddIf<T>(this List<T> source, Predicate<T> match)
    {
        var item = source.Find(match);
        if ( item is not null )
        {
            source.Add(item);
        }
    }

    public static void AddOrUpdateIf<T>(this List<T> source,Predicate<T> match, T newValue)
    {
        var index = source.FindIndex(match);
        if ( index < 0 )
        {
            source.Add(newValue);
        }
        else
        {
            source[index] = newValue;
        }
    }
}
