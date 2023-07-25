using Bizer.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bizer.AspNetCore;
/// <summary>
/// <see cref="IHttpContextAccessor"/> 的主体访问器。
/// </summary>
internal class HttpContextPrincipalAccessor : CurrentPrincipalAccessorBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return _httpContextAccessor.HttpContext!.User;
    }
}
