using Microsoft.Extensions.Hosting;

namespace Bizer.AspNetCore;

public class BizerMiddlewareOptions(IHostEnvironment environment)
{
    /// <summary>
    /// 开启访问 swagger 的路由，即访问 /swagger 。 默认仅 Development 环境开启。
    /// </summary>
    public bool EnableSwaggerPath { get; set; } = environment.IsDevelopment();
    /// <summary>
    /// 开启访问 redoc 的路由，即访问 /redoc 。 默认仅 Production 环境开启。
    /// </summary>
    public bool EnableRedocPath { get; set; } = environment.IsProduction();

    /// <summary>
    /// 获取 <see cref="IHostEnvironment"/> 实例。
    /// </summary>
    public IHostEnvironment Environment { get; private set; } = environment;
}
