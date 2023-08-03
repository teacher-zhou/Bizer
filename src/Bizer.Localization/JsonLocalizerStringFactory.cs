using Microsoft.Extensions.Localization;
using System.Text.Json;

namespace Bizer.Localization;
/// <summary>
/// 本地格式化器的工厂。
/// </summary>
internal class JsonLocalizerStringFactory : IStringLocalizerFactory
{
    private readonly LocalizationOptions _options;
    internal static List<LocalizerStructure> LocalizerCache { get; } = new();

    public JsonLocalizerStringFactory(LocalizationOptions options)
    {
        _options = options;
        Sync();
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(this,_options);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(this, _options);
    }

    /// <summary>
    /// 同步资源包。
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    internal void Sync()
    {
        var file = new FileInfo(_options. LocalizeFilePath);
        if ( !file.Exists )
        {
            throw new FileNotFoundException($"没有找到本地化文件：{_options.LocalizeFilePath}");
        }

        using var stream = file.OpenRead();

        var strcuture = JsonSerializer.Deserialize<LocalizerStructure>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if ( strcuture is null )
        {
            throw new InvalidOperationException($"无法反序列化文件'{_options.LocalizeFilePath}'，请确保文件的结构符合定义");
        }
        LocalizerCache.AddOrUpdateIf(m => m.Culture.Equals(strcuture.Culture), strcuture);
    }
}
