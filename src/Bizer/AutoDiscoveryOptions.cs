using System.Reflection;

namespace Bizer;

/// <summary>
/// 自动发现的配置。
/// </summary>
public class AutoDiscoveryOptions
{
    private ICollection<string> _assemblyNamePatterns = new HashSet<string>();
    private ICollection<Assembly> _assemblies = new HashSet<Assembly>();

    /// <summary>
    /// 获取自动发现服务的程序集集合。
    /// </summary>
    public IEnumerable<Assembly> DiscoveryAssemblies => _assemblies;

    /// <summary>
    /// 获取自动发现服务的程序集匹配名称集合。
    /// </summary>
    public IEnumerable<string> DiscoveryAssembyNamePatterns => _assemblyNamePatterns;

    /// <summary>
    /// 添加要自动发现的程序集名称。
    /// </summary>
    /// <param name="name">程序集名称，支持通配符，但不包含 .dll 后缀。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> 是 <c>null</c> 或空白字符串。</exception>
    public AutoDiscoveryOptions Add(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
        _assemblyNamePatterns.Add(name);
        return this;
    }

    /// <summary>
    /// 添加要自动发现的程序集。
    /// </summary>
    /// <param name="assembly">程序集。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="assembly"/> 是 <c>null</c>。</exception>
    public AutoDiscoveryOptions Add(Assembly assembly)
    {
        if (assembly is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }
        _assemblies.Add(assembly);
        return this;
    }

    /// <summary>
    /// 获取所有匹配的程序集。
    /// </summary>
    /// <returns>匹配自动发现的程序集集合。</returns>
    public IEnumerable<Assembly> GetMatchesAssemblies()
    {
        var assemblyFileList = new List<string>();

        foreach (var assemblySearchName in DiscoveryAssembyNamePatterns)
        {
            var assemblyFiles = Directory.EnumerateFiles($"{AppContext.BaseDirectory}", $"{assemblySearchName}.dll", SearchOption.AllDirectories);
            assemblyFileList.AddRange(assemblyFiles);
        }
        var assemblyList = assemblyFileList.Select(file => Assembly.LoadFile(file)).ToList();

        assemblyList.AddRange(DiscoveryAssemblies);
        return assemblyList;
    }
}
