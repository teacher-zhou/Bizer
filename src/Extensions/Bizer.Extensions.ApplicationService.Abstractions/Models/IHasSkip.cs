namespace Bizer.Extensions.ApplicatonService.Abstractions;

/// <summary>
/// 定义具备查询时的跳过输入。
/// </summary>
public interface IHasSkip
{
    /// <summary>
    /// 获取跳过的记录数。<c>null</c> 表示不跳过。
    /// </summary>
    int? Skip { get; }
}
