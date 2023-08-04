
## Bizer.Services
基于 EntityFrameworkCore 和 Mapster 快速构建 CURD 的业务逻辑服务

有以下基类：
`ServiceBase`：空基类，只有基本的几个对象，例如日志 `ILogger`
`ServiceBase<TContext>`：控制指定 `TContext` 来操作 EF
`ServiceBase<TContext, TEntity>`：直接用 EF 操作指定实体
`CrudServiceBase`：提供 CRUD 的操作

示例：
```cs
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[InjectService]
public interface IUserService:ICrudService<int,User>
{
}

public class UserService : CrudServiceBase<TestDbContext, int, User>, IUserService
{
	public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}
}

```
在启动程序中：
```cs
services.AddBizer(options => options.Assemblies.Add(typeof(IUserService).Assembly))
	.AddMapper()
	.AddServiceInjection();

//EF 配置
services.AddDbContext<TestDbContext>(options => options.UseInMemoryDatabase("db"));
```

像平时那样使用你的接口即可
```cs

private readonly IUserService _userService;
public DemoClass(IUserService userService)
{
        _userService = userService;
}
var result = await _userService.CreateAsync(new() { Id = 1, Name = "admin" });
```