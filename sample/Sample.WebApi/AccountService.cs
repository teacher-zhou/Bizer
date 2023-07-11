using Bizer;
using Bizer.Http;
using Sample.Services;

namespace Sample.WebApi
{
    public class AccountService : IAccountService
    {
        public Task<Returns<string>> SignInAsync([Form(null)] string username, [Form(null)] string password)
        {
            return Returns<string>.Success("sign in ok").ToResultTask();
        }
    }
}
