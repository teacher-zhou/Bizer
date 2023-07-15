# Bizer
![logo](asset/icon.png)

业务开发人员再也不用担心前后端对接的事情，一个接口，前后端全自动对接。

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