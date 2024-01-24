using Bizer;

namespace Sample.Contracts;

[ApiRoute("api/test", Name = "Users")]
public interface ITestService
{
    [Get("{id}")]
    Task<string> GetWithId([Route] int id);

    [Get(Name = "Query")]
    Task<Returns> GetFromQueryParameter(string name);

    [Post(Name = "asign")]
    Task<Returns<User>> SignInAsync([Body] User model);
}

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
