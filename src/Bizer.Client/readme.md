
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