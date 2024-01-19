using System.Text.Json;

namespace Bizer.Client;
/// <summary>
/// 表示 HTTP 客户端的配置。
/// </summary>
public class HttpClientConfiguration
{
    /// <summary>
    /// HTTP 客户端配置的默认名称。
    /// </summary>
    public readonly static string Default = nameof(Default);

    /// <summary>
    /// 获取或设置 HTTP 配置的名称。
    /// </summary>
    public string Name { get; set; } = Default;

    /// <summary>
    /// 获取或设置发送请求时使用的 Internet 资源的统一资源标识符(URI)的基址。
    /// </summary>
    public Uri? BaseAddress { get; set; }

    /// <summary>
    /// 获取每一次 HTTP 请求的一个委托处理集合。
    /// </summary>
    public IList<Func<IServiceProvider, DelegatingHandler>> DelegatingHandlers { get; } = new List<Func<IServiceProvider, DelegatingHandler>>();

    /// <summary>
    /// 获取或设置基于 <see cref="HttpClientHandler"/> 的函数。
    /// </summary>
    public Func<HttpClientHandler>? PrimaryHandler { get; set; }

    /// <summary>
    /// 定义 HTTP 响应的处理结果的委托。
    /// </summary>
    public Func<HttpResponseMessage, Task<Stream>> ResponseHandler { get; set; } = response =>
    {
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStreamAsync();
    };
}
