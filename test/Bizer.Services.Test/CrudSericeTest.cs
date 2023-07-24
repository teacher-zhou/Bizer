using Bizer.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Services.Test;
public class CrudSericeTest:TestBase
{
    private readonly IUserService _userService;

    public CrudSericeTest():base()
    {
        _userService = GetService<IUserService>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBizer(options=>options.Assemblies.Add(typeof(IUserService).Assembly))
            .AddMapper()
            .AddServiceInjection()
            .AddDbContext(options => options.UseInMemoryDatabase("db"));

    }

    [Fact]
    public async Task Test_Create()
    {
        var result = await _userService.CreateAsync(new() { Id = 100, Name = "admin" });
        Assert.NotNull(result);
        Assert.True(result.Succeed);
        Assert.Equal(100, result.Data.Id);
    }

    [Fact]
    public async Task Test_Update()
    {
        var result = await _userService.CreateAsync(new() { Id = 15, Name = "admin" });
        Assert.True(result.Succeed);

        var updated= await _userService.UpdateAsync(15, new() { Name = "abc" });
        Assert.True(updated.Succeed);

        Assert.NotEqual(result.Data.Name, updated.Data.Name);
    }

    [Fact]
    public async Task Test_Delete()
    {
        var result = await _userService.CreateAsync(new() { Id = 11, Name = "admin" });
        Assert.True(result.Succeed);

        var deleted = await _userService.DeleteAsync(1);
        Assert.True(deleted.Succeed);


        var user = await _userService.GetAsync(1);
        Assert.NotNull(user);
        Assert.Null(user.Data);
    }

    [Fact]
    public async Task Test_Get()
    {
        var result = await _userService.CreateAsync(new() { Id = 1, Name = "admin" });
        Assert.True(result.Succeed);

        var user = await _userService.GetAsync(1);
        Assert.NotNull(user);
        Assert.Equal(1,user.Data.Id);
        Assert.Equal("admin", user.Data.Name);
    }


    [Fact]
    public async Task Test_GetList()
    {
        await _userService.CreateAsync(new() { Id = 21, Name = "admin" });
        await _userService.CreateAsync(new() { Id = 22, Name = "admin" });
        await _userService.CreateAsync(new() { Id = 23, Name = "admin" });
        await _userService.CreateAsync(new() { Id = 24, Name = "admin" });

        var user = await _userService.GetListAsync();
        Assert.NotNull(user);
        Assert.NotEmpty(user.Data.Items);
    }
}