

using Bizer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Sample.Contracts;

var builder = Host.CreateDefaultBuilder(args);


builder.ConfigureServices(services =>
{
    services.AddLogging(builder => builder.AddDebug());
    services.AddBizer(options =>
    {
        options.AddAssmebly(typeof(ITestService).Assembly);
    })
    .AddDynamicHttpProxy("http://localhost:5216");


}).ConfigureLogging(log =>
{
    log.AddDebug().AddConsole().AddFilter(level => level == LogLevel.Debug);
});
var app = builder.Build();
app.Start();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var testService = app.Services.GetRequiredService<ITestService>();

#region GET 请求

(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetFromQueryParameter), "")).Assert(logger);
(await app.InvokeMethodAsync<ITestService, Returns>(nameof(ITestService.GetFromQueryParameter), "abc")).Assert(logger);
(await app.InvokeMethodAsync<ITestService, string>(nameof(ITestService.GetWithId), 1000)).Assert(logger);
(await app.InvokeMethodAsync<ITestService, Returns<User>>(nameof(ITestService.SignInAsync), new User { UserName = "admin", Password = "123" })).Assert(logger);
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

    public static void Assert(this object value, ILogger logger)
    {
        logger.LogDebug($"【Result】{value}");
    }

    public static void Assert(this Returns returns, ILogger logger, bool throwIfNotSuccess = true)
    {
        logger.LogDebug($"【Returns】{nameof(Returns.Messages)}：{string.Join("；", returns.Messages)}");
        logger.LogDebug($"【Returns】{nameof(Returns.Succeed)}：{returns.Succeed}");

        if (!returns.Succeed && throwIfNotSuccess)
        {
            throw new InvalidOperationException($"{nameof(Returns.Succeed)} 是 false");
        }
    }

    public static void Assert<TResult>(this Returns<TResult> returns, ILogger logger, bool throwIfNotSuccess = true)
    {
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Data)}：{returns.Data}");
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Messages)}：{string.Join("；", returns.Messages)}");
        logger.LogDebug($"【Returns<TResult>】{nameof(Returns<TResult>.Succeed)}：{returns.Succeed}");

        if (!returns.Succeed && throwIfNotSuccess)
        {
            throw new InvalidOperationException($"{nameof(Returns.Succeed)} 是 false");
        }
    }
}