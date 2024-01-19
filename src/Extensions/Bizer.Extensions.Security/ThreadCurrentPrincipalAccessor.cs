using System.Security.Claims;

namespace Bizer.Extensions.Security;
/// <summary>
/// 表示以线程方式的当前主体访问器。
/// </summary>
public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase
{
    /// <summary>
    /// 获取线程的主体。
    /// </summary>
    /// <exception cref="InvalidCastException"><see cref="Thread.CurrentPrincipal"/> 无法转换成 <see cref="ClaimsPrincipal"/> 对象。</exception>
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return Thread.CurrentPrincipal as ClaimsPrincipal ?? throw new InvalidCastException($"{nameof(Thread.CurrentPrincipal)} 无法转换成 {nameof(ClaimsPrincipal)}");
    }
}
