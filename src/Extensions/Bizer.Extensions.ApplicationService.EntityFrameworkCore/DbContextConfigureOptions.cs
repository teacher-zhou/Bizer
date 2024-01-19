namespace Bizer.Extensions.ApplicationService.EntityFrameworkCore;
/// <summary>
/// 表示对 <see cref="DbContext"/> 选项配置。
/// </summary>
public class DbContextConfigureOptions
{
    /// <summary>
    /// 获取或设置 <see cref="DbContext"/> 选项的配置委托。
    /// </summary>
    public Action<DbContextOptionsBuilder>? ConfigureOptionBuilder { get; set; }
}
