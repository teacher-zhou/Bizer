using Bizer;

namespace Sample.Contracts;

[ApiRoute("api/test")]
public interface ITestService
{
    [Get("{id}")]
    Task<string> GetWithId([Route] int id);

    [Get]
    Task<Returns> GetFromQueryParameter(string name);

    [Post]
    Task<Returns<User>> SignInAsync([Body] User model);
}

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
