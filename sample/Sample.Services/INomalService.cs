using Bizer;

namespace Sample.Services;
[ApiRoute("api/test",Name ="Test")]
public interface INomalService
{
    [Get]
    public Task Get();
    [Post]
    public Task Post();
    [Put]
    public Task Put();

    [Delete]
    public Task Delete();
}