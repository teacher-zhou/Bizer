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
    List<Assembly> Assemblies { get; set; } = new List<Assembly>();

    /// <summary>
    /// 获取或设置自动发现服务的程序集匹配名称集合。
    /// <para>
    /// 支持通配符，但不包含 .dll 后缀。
    /// </para>
    /// </summary>
    List<string> AssemblyNames { get; set; } = new List<string>();

    /// <summary>
    /// 添加能被自动发现服务的程序集。
    /// </summary>
    public AutoDiscoveryOptions AddAssmebly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
        Assemblies.Add(assembly);
        return this;
    }

    /// <summary>
    /// 添加能被自动发现服务的程序集名称。。
    /// <para>
    /// 支持通配符，但不包含 .dll 后缀。如：System.*
    /// </para>
    /// </summary>
    public AutoDiscoveryOptions AddAssembly(params string[]? assemblyNames)
    {
        ArgumentNullException.ThrowIfNull(assemblyNames, nameof(assemblyNames));
        AssemblyNames.AddRange(assemblyNames);
        return this;
    }
    /// <summary>
    /// 获取发现的程序集。
    /// </summary>
    /// <returns>自动发现的程序集集合。</returns>
    public IEnumerable<Assembly> GetDiscoveredAssemblies()
    {
        return FindAssmblies(AssemblyNames, Assemblies);


        static IEnumerable<Assembly> FindAssmblies(IEnumerable<string> assemblNames, IEnumerable<Assembly> assemblies)
        {
            if (assemblNames is null)
            {
                throw new ArgumentNullException(nameof(assemblNames));
            }

            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }
            return assemblNames.SelectMany(assemblySearchName => Directory.EnumerateFiles($"{AppContext.BaseDirectory}", $"{assemblySearchName}.dll", SearchOption.AllDirectories))
                .Select(Assembly.LoadFile)
                .Concat(assemblies)
                .Distinct()
                .OrderBy(m => m.FullName);
        }
    }
}
