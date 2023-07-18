# Bizer
![](asset/bizer.png)

全栈开发自动对接框架，一个人也能轻松完成前后端分离。**简单易用，对旧项目依然有效，开发效率倍增**。

# 快速上手
## :warning: 定义服务接口和 HTTP 路由规则
安装 `Bizer` 包
```ps
> Install-Package Bizer
```
定义业务接口
```cs
[ApiRoute("api/account")] //定义 API 的路由，类似于 Controller
public interface IAccountService
{
	[Post] //定义为 Post 请求
	Task<Returns<string>> SignInAsync([Form]string userName, [Form]string password); //参数使用 form 方式
}
```

就像平时那样的实现自己的业务逻辑即可
```cs
public class AccountApplicationService : IAccountService
{
	public Task<Returns<string>> SignInAsync(string userName, string password)
	{
		//业务逻辑代码

		return Returns<string>.Success(token);
	}
}
```

## :pushpin: 在 AspNetCore 项目中
安装 `Bizer.AspNetCore` 包
```ps
> Install-Package Bizer.AspNetCore
```
配置服务，自动发现并秒变 API
```cs
builder.Services
	.AddBizer(options => options.Assemblies.Add(typeof(IAccountService).Assembly))
	.AddApiConvension();
```
配置中间件，内置 `Swagger`
```cs
app.UseBizerOpenApi();
```
最后启动并访问你的 swagger `http://{localhost:port}/swagger`

## :shield: 在任意客户端项目，例如 Console/Blazor 等
安装 `Bizer.Client` 包
```ps
> Install-Package Bizer.Client
```
注册客户端服务
```cs
builder.Services
	.AddBizer(options => options.Assemblies.Add(typeof(IAccountService).Assembly))
	.AddHttpClientConvension("http://localhost:port");
```
调用方法即可自动发送 HTTP 请求
```cs
private readonly IAccountService _accountService;
public MyClientService(IAccountService accountService)
{
	_accountService = accountService;
}
//...

var result = _accountService.SignInAsync("admin", "password");
if(!result.Succeed)
{
	//result.Errors 获取错误信息
}

var data = result.Data; //获取数据
```

**可以使用同样的方式访问各大 OPEN API 平台，例如微信公众平台、支付宝商户平台等等**


## :computer: 支持环境
* .NET 6
* .NET 7

## :page_with_curl: 许可证
[Apache License 2.0](https://apache.org/licenses/LICENSE-2.0)


# 文档

## Bizer

### 路由
接口定义 `ApiRouteAttribute`，实现 HTTP 的路由前缀，同 `Controller` 里的 `HttpRouteAttribute`
```cs
[ApiRoute("api/users")]	//生成 http://localhost/api/users
public interface IUserManager
{
}
```
**不支持 mvc 中的 `[controller]` 关键字**
> 没有定义该特性的接口不会自动识别成 API 和发送 HTTP 请求。


### Http 方法（HttpMethod)
接口方法上定义，同 MVC 方式使用：以下是对照表格

| Mvc | Bizer |
|---|---|
|HttpGet|Get|
|HttpPost|Post|
|HttpPut|Put|
|HttpDelete|Delete|
|HttpPatch|Patch|
|HttpOptions|Options|
|HttpTrace|Trace|

示例：
```cs
[ApiRoute("api/users")]
public interface IUserService
{
	[Post]
	Task CreateAsync()
}
```
### 参数
参数默认是 `query string`，即 `?arg1=value1&arg2=value2...`
类似于 mvc 的 `FromQueryAttribute`，映射关系如下：
|Mvc|Bizer|备注|
|---|---|---|
|FromRoute|Path|路由中可模板参数，如{id}|
|FromQuery|Query|
|FromHeader|Header|自动加入到 Header 中
|FromForm|Form|会自动使用 form/data 方式|
|FromBody|Body|用 body 提交，默认使用 application/json 的方式|
示例：
```cs
[ApiRoute("api/users")]
public interface IUserService
{
	[Post]
	Task CreateAsync([Body]User user)

	[Get("{id}")]
	Task<User> GetAsync([Path]int id)
}
```
### 配置
在 `Program.cs` 中注册服务和配置：
```cs
services.AddBizer(options=>{
	//配置自动程序集发现，目的是为之后的模块使用

	options.Assemblies.Add(typeof(xxx).Assembly); //添加自动发现的程序集

	options.AssemblyNames.Add("MyAssembly.*.Service");//模糊搜索匹配名称的程序集，支持通配符
});
```
### `Returns` 和 `Resunts<TResult>` 返回值类型
该类型将返回 `Code` `Messages` `Succeed` `Data` 四个基本属性。
```cs
public Task<Returns> GetAsync() //无返回数据
{
	if(xxxx)
	{
		return Returns.Failed("错误信息");
	}
	return Returns.Success();
}

public Task<Returns<Data>> GetAsync() //有返回数据
{
	if(xxxx)
	{
		return Returns<Data>.Failed("错误信息");
	}
	return Returns<Data>.Success(data);//一般是成功后才设置数据返回
}


public Task<Returns> GetAsync() //可以返回多消息
{
	var returns = new Returns();
	if(xxxx)
	{
		returns.AppendMessages("....");
	}
	else if(xxx)
	{
		returns.AppendMessages("....");
	}
	else
	{
		returns.IsSuccess();
	}
	return returns;
}
```

## Bizer.Client
自动识别定义了 `ApiRouteAttribute` 的接口，在方法调用时自动发送 HTTP 请求。

唯一必须要配置的是 `BaseAddress`
```cs
services.AddBizer(options=>{
	options.Assemblies.Add(typeof(xxx).Assembly); //添加自动发现的程序集
	options.AssemblyNames.Add("MyAssembly.*.Service");//模糊搜索匹配名称的程序集，支持通配符
})
.AddHttpClientConvension("http://localhost:port"); // 这里是 BaseAddress
```

**PS：不需要刻意注册接口的服务**

#### HttpClientConfiguration
添加 `DelegatingHandler` 委托
```cs
AddHttpClientConvension(configure=>{
	configure.BaseAddress
	configure.DelegatingHandlers.Add(provider => new MyHttpClientHandler());
});
```

参考 [https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/httpclient-message-handlers](https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/httpclient-message-handlers)
```cs
public class MyHttpClientHandler : DelegatingHandler
{
	public MyHttpClientHandler(HttpMessageHandler innerHandler) : base(innerHandler)
	{
	}

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return base.SendAsync(request, cancellationToken);
	}
	protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return base.Send(request, cancellationToken);
	}
}
```

## Bizer.AspNetCore
让后端的业务逻辑层服务可以秒变 Web API

```cs
services.AddBizer(options=>{
	options.Assemblies.Add(typeof(xxx).Assembly); //添加自动发现的程序集
	options.AssemblyNames.Add("MyAssembly.*.Service");//模糊搜索匹配名称的程序集，支持通配符
})
.AddApiConvension(); //添加这个服务即可
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

有任何问题请使用【[Github Issue](https://github.com/AchievedOwner/Bizer/issues)】提出