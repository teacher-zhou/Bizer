namespace Sample.Contracts.Impl;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Bizer;

public class AuthService : IAuthService
{
    public Task<string> SignInAsync()
    {
        ClaimsIdentity identity = new("Auth", ClaimTypes.Name, ClaimTypes.Role);
        identity.AddClaim(new(ClaimTypes.Name, "admin"));

        JwtSecurityToken token = new(claims: identity.Claims);

        JwtSecurityTokenHandler handler = new();
        return handler.WriteToken(token).AsTask();
    }
}
