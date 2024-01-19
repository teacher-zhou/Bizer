# Bizer
Bizer.Core 用于定义前后端 HTTP 的解析规范。
根据需要安装：
- 动态 HTTP 客户端 Bizer.Client: 根据接口的 HTTP 定义，自动发送 Http 请求
- 动态 API 服务端 Bizer.AspNetCore: 根据接口的 HTTP 定义，自动变成 WEB API 接口

## :rainbow: 优势
- 只需要定义一套接口，就可以完成前后端对接，减少重复工作量，不再担心 API 接口的规范不一致；
- 完美兼容任意项目，只需要给接口定义框架的 HTTP 规范即可；
- 非常灵活，可以将接口和实现作为普通的方法，根据选择配套安装 `Bizer.AspNetCore` 或 `Bizer.Client` 即可实现动态 API 或动态 Http 的功能；

## :smile: 概念图
![](asset/bizer.png)


# 快速上手
## :warning: 定义服务接口和 HTTP 路由规则
安装 `Bizer` 包
```ps
> Install-Package Bizer
```
定义接口
```diff
+[ApiRoute("api/account")] //定义 API 的路由基础路由
public interface IAccountService
{
+	[Post] //定义为 Post 请求
	Task<Returns<string>> SignInAsync([Form]string userName, [Form]string password); //参数使用 form 方式

+ 	[Get("{id}")]//定义 Get 请求，路由为：api/account/{id}
	Task<MyData> GetAsync(int id);

+	[Put("student/{id}")] // 定义 Put 请求，路由为：api/account/{id}
	Task UpdateStudentAsync([Body]Student model, int id);
}
```

实现接口
```cs
public class AccountApplicationService : IAccountService
{
	public Task<Returns<string>> SignInAsync(string userName, string password)
	{
		//...业务逻辑代码
		return Returns<string>.Success(token);
	}

	//...其他实现

	[Authorize(Roles = "admin")] //兼容 Microsoft.AspNetCore.Authorization 认证授权框架
	Task UpdateStudentAsync([Body]Student model, int id)
	{
		//...
	}
}
```

## :pushpin: 动态 WEB API
在 ASP.NET CORE 项目中安装 `Bizer.AspNetCore` 包
```ps
> Install-Package Bizer.AspNetCore
```
配置服务
```diff
builder.Services
	.AddBizer(options => options.AddAssembly(typeof(IAccountService).Assembly)) //要扫描的程序集
+	.AddDynamicWebApi(); //将扫描到的程序集的接口和实现秒变动态 WEB API 服务
```

**自行安装相关的 Swagger 包即可看到生成的 API or 通过 Postman 来模拟发送 HTTP 测试 API 接口**

## :shield: 动态 Http 代理

安装 `Bizer.Client` 包
```ps
> Install-Package Bizer.Client
```
配置要作为动态 HTTP 代理所在接口的程序集
```diff
builder.Services
	.AddBizer(options => options.AddAssembly(typeof(IAccountService).Assembly)) //要扫描的程序集
+	.AddDynamicHttpProxy("http://{localhost}:{port}"); // 将扫描到的程序集的接口秒变动态 HTTP 代理
```

在任意地方调用接口方法：
```cs
private readonly IAccountService _accountService;
public MyClientService(IAccountService accountService)
{
	_accountService = accountService;
}
//...
public async Task ProcceedAsync()
{
	var result = _accountService.SignInAsync("admin", "password");
	if(!result.Succeed)
	{
		//result.Errors 获取错误信息
	}

	var data = result.Data; //获取数据
}
```

## :computer: 支持环境
* .NET 6
* .NET 7
* .NET 8

## :page_with_curl: 许可证
[Apache License 2.0](https://apache.org/licenses/LICENSE-2.0)

## :toolbox: API 路由和请求对照表
|Bizer|Controller|说明|
|-|-|-|
|ApiRoute|Route|路由模板|
|Get|HttpGet|Get 请求|
|Post|HttpPost|Post 请求|
|Put|HttpPut|Put 请求|
|Delete|HttpDelete|Delete 请求|
|Patch|HttpPatch|Patch 请求|
|Options|HttpOptions|Options 请求|

## :fire: API 参数定义对照表
|Bizer|Controller|说明|
|-|-|-|
|Header|FromHeader|参数是从 Header 中传递|
|Form|FromForm|参数是从 Form 中传递|
|Body|FromBody|参数是从 Json 中传递|
|Route|FromRoute|参数从路由中传递|
|Query|FromQuery|参数从查询字符串传递|

## :question: Q & A
- 为什么我的接口没有识别成 API？
只有接口定义了 `ApiRouteAttribute` 特性，才会被自动识别动态 API 或 HTTP 代理
- 我接口定义了 `ApiRouteAttribute` 特性，但是方法却没有识别成 API
接口中定义的方法，需要定义请求类型才会被正确识别成 API

使用过程中遇到的问题可在【[Github Issue](https://github.com/AchievedOwner/Bizer/issues)】提出

## :clapper: 扩展
扩展可以根据自己的需要安装
### `Bizer.Extensions.AutoInjection` 
实现接口的自动化注入。接口中使用 `InjectServiceAttribute` 即可：

```diff
+[InjectService] //默认是 Scoped
public interface IMyService { }

+[InjectService(Lifetime = ServiceLifetime.Transient)] //注册成 Transient 生命周期
public interface IMyService { }
```
添加扩展模块：
```diff
builder.Services.AddBizer(...)
+		.AddServiceInjection() //扫描到的程序集中，接口定义了 InjectService 特性都会被添加成服务(IoC)
```

### `Bizer.Extensions.ApplicationService.EntityFrameworkCore`
基于 EntityFrameworkCore 实现的应用服务，并使用 Mapster 实现对象映射。

> 类似于 Abp 的 ApplicationService 层，但比它更简单。

```diff
builder.Services.AddBizer(...)
+		.AddMapster() //添加 Mapster 服务

		//使用内置的 BizerDbContext 作为项目
+		.AddDbContext(options => options.UseSqlServer("...")) 

		//或者使用自定义的 DbContext，要求继承自 BizerDbContext
+		.AddDbContext<MyDbContext>(options => options.UseSqlServer("..."))
```

- 接口可以继承自 `IQueryService`(只读查询服务) 或 `ICrudService`(CRUD 服务)
```cs
public interface IUserQueryService : IQuerySerivce<Guid, UserQueryDto> { }

public interface IUserService : ICrudService<Guid, UserQueryDto, UserListDto, UserCreateDto, UserUpdateDto>
```

- 对应实现则是 `QueryServiceBase` 和 `CrudServiceBase` 两个基类
```cs
public class UserQueryService : IUserQueryService, QueryServiceBase<Guid, User, UserQueryDto> 
{ 
	//...
}

public class UserService : IUserService, CrudServiceBase<Guid, User, UserQueryDto, UserListDto, UserCreateDto, UserUpdateDto>
{
	//...
}
```

- 实体的配置需要实现接口 `IEntityTypeConfiguration<TEntity>` 使用 FluentAPI 配置
```cs
public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(ModelBuilder<User> builder)
	{
		builder.HasKey(m=>m.Id);
		builder.Property(m=>m.Name).IsRequired();
		//...
		builder.ToTable("MyUserTable");
	}
}
```
**内置的 `BizerDbContext` 会自动根据程序集将实现 `IEntityTypeConfiguration<TEntity>` 的配置加入到 `DbContext` 中**，无需添加 `DbSet`

[有关 EntityFrameworkCore 的 Fluent API 配置](https://learn.microsoft.com/en-us/ef/core/modeling/#use-fluent-api-to-configure-a-model)

- 你也可以实现自己的 `DbContext`，或继承自 `BizerDbContext`，如果不继承 `BizerDbContext` 就不会有自动配置功能，可按照常规方式自己添加或用 `DbSet`

```cs
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions options) : base(options) { }

	public DbSet<User> Users { get; set; }
}
```
那么 `QueryServiceBase` 或 `CrudServiceBase` 需要显示地指定你的 `DbContext` 对象：
```diff
+ public class UserQueryService : IUserQueryService, QueryServiceBase<AppDbContext, Guid, User, UserQueryDto> 
- public class UserQueryService : IUserQueryService, QueryServiceBase<Guid, User, UserQueryDto> 
{ 
	//...
}

+ public class UserService : IUserService, CrudServiceBase<AppDbContext, Guid, User, UserQueryDto, UserListDto, UserCreateDto, UserUpdateDto>
- public class UserService : IUserService, CrudServiceBase<Guid, User, UserQueryDto, UserListDto, UserCreateDto, UserUpdateDto>
{
	//...
}
```

最终在 `Program.cs` 中修改
```diff
builder.Services.AddBizer(...)
		.AddMapster()
-		.AddDbContext(options => options.UseSqlServer("...")) 
+		.AddDbContext<AppDbContext>(options => options.UseSqlServer("..."))
```