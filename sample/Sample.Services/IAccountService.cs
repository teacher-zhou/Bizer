using Bizer;
using Bizer.Http;

namespace Sample.Services;
[ApiRoute("api/account")]
public interface IAccountService
{
    [Post("sign-in")]
    public Task<Returns<string>> SignInAsync([Form]string username, [Form]string password);
}