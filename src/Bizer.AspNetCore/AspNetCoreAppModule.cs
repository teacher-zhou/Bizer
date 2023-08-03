using Microsoft.AspNetCore.Builder;

namespace Bizer.AspNetCore;
/// <summary>
/// 继承以实现 AspNetCore 模块化的配置。
/// </summary>
public abstract class AspNetCoreAppModule : AppModule
{
    /// <summary>
    /// 配置应用程序请求的管道。
    /// </summary>
    /// <param name="app">应用程序请求的管道。</param>
    public virtual void ConfigureApplications(IApplicationBuilder app) { }
}
