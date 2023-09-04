using Bizer.Services;
using Sample.Services;

namespace Sample.WebApi
{
    public class UserService : CrudServiceBase<TestDbContext, int, User>, IUserService
    {
        //public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
        //}
    }
}
