## Bizer.AspNetCore
让后端的业务逻辑层服务可以秒变 Web API

```cs
services.AddBizer(options=>{
	options.Assemblies.Add(typeof(xxx).Assembly); //添加自动发现的程序集
	options.AssemblyNames.Add("MyAssembly.*.Service");//模糊搜索匹配名称的程序集，支持通配符
})
.AddOpenApiConvension(); //添加这个服务即可
```
中间件新增
```cs
app.UseBizerOpenApi();
```

**接口必须要有实现，就像你的业务层一样**

示例
```cs
[ApiRoute("api/account")]
public interface IAccountApplicationService
{
	[Get]
	Task<User> GetAsync(int id);
}

public class AccountApplicationService : IAccountApplicationService
{
	private readonly AppDbContext _context;
	public AccountApplicationService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<User> GetAsync(int id)
	{
		var user = await _context.Users.FindAsync(id);
		//...
		return user;
	}
}
```