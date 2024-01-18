using Microsoft.Extensions.Hosting;

namespace Bizer.AspNetCore;

/// <summary>
/// Bizer 中间件配置。
/// </summary>
public class BizerMiddlewareOptions
{
    private readonly IHostEnvironment _environment;

    /// <summary>
    /// 初始化 <see cref="BizerMiddlewareOptions"/> 类的新实例。
    /// </summary>
    /// <param name="environment">环境对象。</param>
    public BizerMiddlewareOptions(IHostEnvironment environment)
    {
        _environment = environment;

        EnableSwaggerPath = _environment.IsDevelopment();
        EnableRedocPath = _environment.IsProduction();
    }

    /// <summary>
    /// 开启访问 swagger 的路由，即访问 /swagger 。 默认仅 Development 环境开启。
    /// </summary>
    public bool EnableSwaggerPath { get; set; }
    /// <summary>
    /// 开启访问 redoc 的路由，即访问 /redoc 。 默认仅 Production 环境开启。
    /// </summary>
    public bool EnableRedocPath { get; set; }

    /// <summary>
    /// 获取 <see cref="IHostEnvironment"/> 实例。
    /// </summary>
    public IHostEnvironment Environment => _environment;
}
