using System.Security.Claims;

namespace Bizer.Security;
/// <summary>
/// 定义当前的主体访问器。
/// </summary>
public interface ICurrentPrincipalAccessor
{
    /// <summary>
    /// 获取用户主体。
    /// </summary>
    ClaimsPrincipal User { get; }
}
