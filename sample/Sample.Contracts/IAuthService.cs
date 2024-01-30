using Bizer;

namespace Sample.Contracts;

[ApiRoute("api/auth")]
public interface IAuthService
{
    [Post]
    Task<string> SignInAsync();
}
