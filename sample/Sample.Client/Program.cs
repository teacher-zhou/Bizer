

using Bizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Client;
using Sample.Services;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddLogging(builder=>builder.AddDebug());
    services.AddBizer(options =>
    {
        options.Assemblies.Add(typeof(ITestService).Assembly);
        //options.AssembyNames.Add("Sample.*");
    })
    .AddHttpClientConvension("http://localhost:5192");
}).ConfigureLogging(log =>
{
    log.AddDebug().AddConsole().AddFilter(level=>level== LogLevel.Debug);
});
var app = builder.Build();
app.Start();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var testService = app.Services.GetRequiredService<ITestService>();

#region GET 请求
//无参数
(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetAsync))).Assert(logger);

//path 中的参数
(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetFromPathAsync), 1000)).Assert(logger);

//query 中的参数
(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetFromQueryAsync), "张三")).Assert(logger);
//header 中的参数
(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetFromHeaderAsync), "Token")).Assert(logger);

//无参数有返回值
(await app.InvokeMethodAsync<ITestService, Returns<string>>(nameof(ITestService.GetHasData))).Assert(logger);
#endregion

#region Post

//await app.InvokeMethodAsync<ITestService, string>(nameof(ITestService.PostAsync),"asdasdasd");
await app.InvokeMethodAsync<ITestService>(nameof(ITestService.PostNothingAsync));
#endregion

#region Put

app.InvokeMethod<ITestService, Returns<string>>(nameof(ITestService.PutData), "name").Assert(logger);
app.InvokeMethod<ITestService>(nameof(ITestService.PutNothing), 123);
#endregion

public static class AssertExtensions
{
    public static Task<TResult> InvokeMethodAsync<TService, TResult>(this IHost app, string methodName, params object?[] parameters)
    {
        var serviceType = typeof(TService);
        var service = app.Services.GetRequiredService(serviceType);
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        logger.LogDebug("============ {0} ================", methodName);
        logger.LogDebug("方法的参数：{0}", string.Join(", ", parameters));
        return (Task<TResult>)serviceType.GetMethod(methodName)?.Invoke(service, parameters);
    }

    public static Task InvokeMethodAsync<TService>(this IHost app, string methodName, params object?[] parameters)
    {
        var serviceType = typeof(TService);
        var service = app.Services.GetRequiredService(serviceType);
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        logger.LogDebug("============ {0} ================", methodName);
        logger.LogDebug("方法的参数：{0}", string.Join(", ", parameters));
        return (Task)serviceType.GetMethod(methodName)?.Invoke(service, parameters);
    }
    public static TResult InvokeMethod<TService, TResult>(this IHost app, string methodName, params object?[] parameters)
    {
        var serviceType = typeof(TService);
        var service = app.Services.GetRequiredService(serviceType);
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        logger.LogDebug("============ {0} ================", methodName);
        logger.LogDebug("方法的参数：{0}", string.Join(", ", parameters));
        return (TResult)serviceType.GetMethod(methodName)?.Invoke(service, parameters);
    }
    public static void InvokeMethod<TService>(this IHost app, string methodName, params object?[] parameters)
    {
        var serviceType = typeof(TService);
        var service = app.Services.GetRequiredService(serviceType);
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        logger.LogDebug("============ {0} ================", methodName);
        logger.LogDebug("方法的参数：{0}", string.Join(", ", parameters));
        serviceType.GetMethod(methodName)?.Invoke(service, parameters);
    }

    public static void Assert(this Returns returns, ILogger logger, bool throwIfNotSuccess = true)
    {
        logger.LogDebug($"【Returns】{nameof(Returns.Messages)}：{string.Join("；", returns.Messages)}");
        logger.LogDebug($"【Returns】{nameof(Returns.Succeed)}：{returns.Succeed}");

        if ( !returns.Succeed && throwIfNotSuccess )
        {
            throw new InvalidOperationException($"{nameof(Returns.Succeed)} 是 false");
        }
    }

    public static void Assert<TResult>(this Returns<TResult> returns, ILogger logger, bool throwIfNotSuccess = true)
    {
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Data)}：{returns.Data}");
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Messages)}：{string.Join("；", returns.Messages)}");
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Succeed)}：{returns.Succeed}");

        if ( !returns.Succeed && throwIfNotSuccess )
        {
            throw new InvalidOperationException($"{nameof(Returns.Succeed)} 是 false");
        }
    }
}