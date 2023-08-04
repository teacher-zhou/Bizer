using Microsoft.Extensions.Localization;

namespace Bizer.Localization;

/// <summary>
/// JSON 多语言格式化器。
/// </summary>
internal class JsonStringLocalizer : IStringLocalizer
{
    private readonly IStringLocalizerFactory _factory;
    private readonly LocalizationOptions _options;

    public JsonStringLocalizer(IStringLocalizerFactory factory, LocalizationOptions options)
    {
        _factory = factory;
        _options = options;
    }

    public LocalizedString this[string name] => this[name, Enumerable.Empty<object>()];

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var structure = JsonLocalizerStringFactory.LocalizerCache.Find(m => m.Culture.Equals(_options.Culture));
            if ( structure is null )
            {
                throw new InvalidOperationException($"没有找到语言包 {_options.Culture}");
            }

            if ( !structure.Values.TryGetValue(name, out var value) )
            {
                throw new KeyNotFoundException($"Cannot fine the localize name {name} from localize file {_options.LocalizeFilePath}");
            }
            return new LocalizedString(name, string.Format(value, arguments), true, _options.LocalizeFilePath);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var structure = JsonLocalizerStringFactory.LocalizerCache.Find(m => m.Culture.Equals(_options.Culture));
        if ( structure is null )
        {
            throw new InvalidOperationException($"没有找到语言包 {_options.Culture}");
        }

        return structure.Values.Select(m=>new LocalizedString(m.Key, m.Value));
    }
}
