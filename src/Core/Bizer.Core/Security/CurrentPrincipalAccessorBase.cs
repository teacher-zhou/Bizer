using System.Security.Claims;

namespace Bizer.Security;

/// <summary>
/// 表示当前主体访问器的基类。这是一个抽象类。
/// </summary>
public abstract class CurrentPrincipalAccessorBase : ICurrentPrincipalAccessor
{
    private readonly AsyncLocal<ClaimsPrincipal> _localPrincipal = new();

    /// <summary>
    /// 获取声明主体。
    /// </summary>
    public ClaimsPrincipal User => _localPrincipal.Value ?? GetClaimsPrincipal();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="principal">要更换的主体。</param>
    /// <returns></returns>
    public virtual IDisposable Change(ClaimsPrincipal principal)
    {
        var parent = User;
        _localPrincipal.Value = principal;
        return Disposing.Perform(() =>
        {
            _localPrincipal.Value = parent;
        });
    }

    /// <summary>
    /// 重写以获得声明主体。
    /// </summary>
    protected abstract ClaimsPrincipal GetClaimsPrincipal();
}
