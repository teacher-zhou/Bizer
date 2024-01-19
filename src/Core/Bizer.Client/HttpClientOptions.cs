namespace Bizer.Client;
internal class HttpClientOptions
{
    public Dictionary<Type, HttpClientConfiguration> HttpConfigurations { get; set; } = new();
}
