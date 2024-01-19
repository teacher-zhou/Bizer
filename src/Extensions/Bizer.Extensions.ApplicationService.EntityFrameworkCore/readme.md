
## Bizer.Extensions.ApplicationService.EntityFrameworkCore
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

//使用了 AddDbContext<TestDbContext> 时，需要指定 TContext 参数
public class UserService : CrudServiceBase<TestDbContext, int, User>, IUserService
{
    public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

//配置了 AddDbContext() 时，可以不指定 TContext
public class UserService : CrudServiceBase<int, User>, IUserService
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
    .AddServiceInjection()
    .AddDbContext(options.UseSqlServer("xx"));//使用 BizerDbContext
    // 或者
    .AddDbContext<MyDbContext>(options.UseSqlServer("xx"));//使用自定义 DbContext
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

## 注意事项

在添加服务时，只添加 `AddDbContext()` 方法，默认使用 `BizerDbContext` 的 `DbContext` 类型：

```cs
builder.AddBizer().AddDbContext();
```

- 迁移时，需要使用命令指定 `BizerDbContext`:
```ps
PM> Add-Migration xxxxx
PM> Update-Database -Context BizerDbContext
```

- 编写业务逻辑时，继承的 Crud 基类可以使用不带 `TContext` 泛型参数的

```cs
public class MyClass : CrudServiceBase<Guid, MyEntity>
{
    //...
}
```

如果使用了自定义 `DbContext`

```cs
builder.AddBizer().AddDbContext<MyDbContext>();
```

- 建议派生自 `BizerDbContext` 基类，以确保可以继续使用**自动发现迁移对象的功能**

```cs
public class MyDbContext : BizerDbContext
{
    public MyDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
    {

    }
}
```

- 编写业务逻辑时，继承的 Crud 基类需要显示地带上你自定义的 `DbContext` 泛型参数

```cs
public class MyClass : CrudServiceBase<MyDbContext, Guid, MyEntity>
{
    //...
}
```

## 扩展功能

- 使用 `Mapster` 作为对象映射功能

```cs
builder.AddBizer().AddMapper();
```