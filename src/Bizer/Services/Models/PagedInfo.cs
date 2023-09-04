using System.Text.Json.Serialization;

namespace Bizer.Services;

/// <summary>
/// 表示分页结果的输出信息。
/// </summary>
/// <typeparam name="TItem">数据类型。</typeparam>
public record class PagedInfo<TItem> where TItem : notnull
{
    /// <summary>
    /// 初始化 <see cref="PagedInfo{TItem}"/> 类的新实例。
    /// </summary>
    /// <param name="items">分页结果。</param>
    /// <param name="total">总数据量。</param>
    [JsonConstructor]
    public PagedInfo(IReadOnlyList<TItem> items, long total)
    {
        Items = items;
        Total = total;
    }
    /// <summary>
    /// 获取分页结果。
    /// </summary>
    public IReadOnlyList<TItem> Items { get; }
    /// <summary>
    /// 获取总数据量。
    /// </summary>
    public long Total { get; }
}
