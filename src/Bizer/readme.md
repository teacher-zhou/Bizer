## Bizer
作为定义、规则、扩展的核心类

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
