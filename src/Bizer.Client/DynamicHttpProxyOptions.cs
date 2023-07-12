namespace Bizer.Client;
public class DynamicHttpProxyOptions
{
    public Dictionary<Type, DynamicHttpProxyConfiguration> HttpProxies { get; set; } = new();
}
