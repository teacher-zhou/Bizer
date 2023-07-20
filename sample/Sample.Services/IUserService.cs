using Bizer;
using Bizer.Services;

namespace Sample.Services;
[InjectService]
[ApiRoute("api/users")]
public interface IUserService : ICrudService<int, User>
{
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}