using Bizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Bizer.Client;

/// <summary>
/// 表示对指定的服务进行 HTTP 动态请求的代理服务。
/// </summary>
/// <typeparam name="TService"></typeparam>
internal class DynamicHttpClientProxy<TService>
{
    public DynamicHttpClientProxy(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }
    public IHttpClientFactory HttpClientFactory => ServiceProvider.GetRequiredService<IHttpClientFactory>();

    protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

    public ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

    public DynamicHttpProxyOptions Options => ServiceProvider.GetRequiredService<IOptions<DynamicHttpProxyOptions>>().Value;

    protected DynamicHttpProxyConfiguration Configuration
    {
        get
        {
            var serviceType = typeof(TService);
            var configuration = Options.HttpProxies[serviceType];
            return configuration;
        }
    }

    protected virtual HttpClient CreateClient() => HttpClientFactory.CreateClient(Configuration.Name);

    public virtual async Task<object?> SendAsync(HttpRequestMessage request)
    {
        using var client = CreateClient();
        var response = await client.SendAsync(request);
        return HandleHttpResponseMessageAsync(response);
    }

    /// <summary>
    /// 以异步的方式处理 <see cref="HttpResponseMessage"/> 并解析为 <see cref="Returns"/> 对象。
    /// </summary>
    /// <param name="response">HTTP 请求的响应消息。</param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> 是 null。</exception>
    protected Task<object?> HandleHttpResponseMessageAsync(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        Logger.LogDebug($"Http Status Code:{response.StatusCode}");
        return Configuration.ResponseHandler(response);
    }



}
