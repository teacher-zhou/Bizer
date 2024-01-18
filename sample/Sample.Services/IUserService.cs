using Bizer;
using Bizer.Services.Abstractions;

namespace Sample.Services;
/// <summary>
/// 用户接口
/// </summary>
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