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
    /// 获取发现的程序集。
    /// </summary>
    /// <param name="excludeAssemblies">从自动发现中需要排除的程序集数组。</param>
    /// <returns>自动发现的程序集集合。</returns>
    public IEnumerable<Assembly> GetDiscoveredAssemblies(params Assembly[]? excludeAssemblies)
    {
        var assemblyFileList = new List<string>();

        foreach (var assemblySearchName in AssembyNames)
        {
            var assemblyFiles = Directory.EnumerateFiles($"{AppContext.BaseDirectory}", $"{assemblySearchName}.dll", SearchOption.AllDirectories);
            assemblyFileList.AddRange(assemblyFiles);
        }
        var assemblyList = assemblyFileList.Select(Assembly.LoadFile).ToList();

        assemblyList.AddRange(Assemblies);
        return assemblyList.Except(excludeAssemblies ?? Enumerable.Empty<Assembly>());
    }
}
