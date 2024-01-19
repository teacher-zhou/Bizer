using Bizer;

namespace Sample.Contracts.Impl;

public class TestSerivce : ITestService
{
    public Task<Returns> GetFromQueryParameter(string name)
    {
        return Returns.Success().AsTask();
    }

    public Task<string> GetWithId([Route] int id)
    {
        return $"{id}".AsTask();
    }

    public Task<Returns<User>> SignInAsync([Body] User model)
    {
        return Returns<User>.Success(model).WithMessage("成功").AsTask();
    }
}
