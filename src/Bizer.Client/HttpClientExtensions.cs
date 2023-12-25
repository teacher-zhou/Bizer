using Bizer.Client;

using Castle.DynamicProxy;

namespace System.Net.Http;

public static class HttpClientExtensions
{
    public static TService For<TService>(this HttpClient http, IServiceProvider provider)
    {
        var serviceType = typeof(TService);

        ProxyGenerator Generator = new();

        var interceptorType = typeof(HttpClientInterceptor<>).MakeGenericType(serviceType);

        var interceptor = Activator.CreateInstance(interceptorType, provider, http);
        var target = Generator.CreateInterfaceProxyWithoutTarget(serviceType, ((IAsyncInterceptor)interceptor).ToInterceptor());
        return (TService)target;
    }
}
