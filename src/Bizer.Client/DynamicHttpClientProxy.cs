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

    public virtual async Task<TResult?> SendAsync<TResult>(HttpRequestMessage request)
    {
        var serviceType = typeof(TService);
        var configuration = Options.HttpProxies[serviceType];
        using var client = HttpClientFactory.CreateClient(configuration.Name);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Logger.LogDebug($"返回的 HTTP 状态码：{response.StatusCode}（{(int)response.StatusCode}）");

        var content = await response.Content.ReadAsStringAsync();

        if ( string.IsNullOrEmpty(content) )
        {
            throw new HttpRequestException("要求远程服务必须有返回类型");
        }

        var returns = JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return returns;
    }



}
