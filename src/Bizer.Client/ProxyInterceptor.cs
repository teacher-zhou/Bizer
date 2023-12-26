using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Bizer.Client.Proxy;

namespace Bizer.Client;

public class ProxyInterceptor : IBizerInterceptor
{
    private readonly HttpClient _client;

    public ProxyInterceptor(HttpClient client)
    {
        this._client = client;
    }

    public async Task<object> Intercept(MethodInfo method, object[] parameters)
    {
        var response = await _client.SendAsync(new HttpRequestMessage
        {
            RequestUri = new("/api/test"),
            Method = HttpMethod.Get
        });
        var stream = response.Content.ReadAsStream();


        return JsonSerializer.Deserialize(stream, method.ReturnType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
