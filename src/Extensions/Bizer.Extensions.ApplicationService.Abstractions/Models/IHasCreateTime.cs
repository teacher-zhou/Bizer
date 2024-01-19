namespace Bizer.Extensions.ApplicatonService.Abstractions;

/// <summary>
/// 定义具备创建时间的属性。
/// </summary>
public interface IHasCreateTime
{
    /// <summary>
    /// 获取创建时间。
    /// </summary>
    public DateTime CreateTime { get; }
}
