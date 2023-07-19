namespace Bizer.Stores;

/// <summary>
/// 提供基本工作单元的功能。
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// 提交工作单元所影响的结果。
    /// </summary>
    /// <returns>工作单元提交所影响结果。</returns>
    /// <exception cref="InvalidOperationException">提交过程中出现的操作异常。</exception>
    int Commit();
    /// <summary>
    /// 以异步的方式提交工作单元所影响的结果。
    /// </summary>
    /// <param name="cancellationToken">用于取消工作单元提交的令牌。</param>
    /// <returns>一个包含工作单元提交所影响结果的异步结果。</returns>
    /// <exception cref="InvalidOperationException">提交过程中出现的操作异常。</exception>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}