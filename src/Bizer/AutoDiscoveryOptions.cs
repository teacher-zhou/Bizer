using System.Reflection;

namespace Bizer;

/// <summary>
/// 自动发现的配置。
/// </summary>
public sealed class AutoDiscoveryOptions
{
    /// <summary>
    /// 获取或设置自动发现服务的程序集集合。
    /// </summary>
    public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();

    /// <summary>
    /// 获取或设置自动发现服务的程序集匹配名称集合。
    /// <para>
    /// 支持通配符，但不包含 .dll 后缀。
    /// </para>
    /// </summary>
    public IList<string> AssembyNames { get; set; } = new List<string>();

    /// <summary>
    /// 获取或设置自动发现服务要排除的程序集匹配名称集合。
    /// <para>
    /// 支持通配符，但不包含 .dll 后缀。
    /// </para>
    /// </summary>
    public IList<string> ExcludeAssemblNames { get; set; } = new List<string>();
    /// <summary>
    /// 获取或设置自动发现服务要排除的程序集集合。
    /// </summary>
    public IList<Assembly> ExcludeAssemblies { get; set; } = new List<Assembly>();

    /// <summary>
    /// 获取发现的程序集。
    /// </summary>
    /// <returns>自动发现的程序集集合。</returns>
    public IEnumerable<Assembly> GetDiscoveredAssemblies()
    {
        return FindAssmblies(AssembyNames, Assemblies).Except(FindAssmblies(ExcludeAssemblNames, ExcludeAssemblies));


        static IEnumerable<Assembly> FindAssmblies(IEnumerable<string> assemblNames, IEnumerable<Assembly> assemblies)
        {
            if ( assemblNames is null )
            {
                throw new ArgumentNullException(nameof(assemblNames));
            }

            if ( assemblies is null )
            {
                throw new ArgumentNullException(nameof(assemblies));
            }
            return assemblNames.SelectMany(assemblySearchName => Directory.EnumerateFiles($"{AppContext.BaseDirectory}", $"{assemblySearchName}.dll", SearchOption.AllDirectories))
                .Select(Assembly.LoadFile)
                .Concat(assemblies)
                .Distinct();
        }
    }
}
