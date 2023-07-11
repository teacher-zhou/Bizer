using Castle.DynamicProxy;

namespace Bizer.Client;
internal class HttpClientProxyInterceptor : IAsyncInterceptor
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IRemotingConverter _converter;

    public HttpClientProxyInterceptor(IHttpClientFactory httpFactory,IRemotingConverter converter)
    {
        _httpFactory = httpFactory;
        _converter = converter;
    }

    public void InterceptAsynchronous(IInvocation invocation)
    {
        throw new NotImplementedException();
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        throw new NotImplementedException();
    }

    public void InterceptSynchronous(IInvocation invocation)
    {
        throw new NotImplementedException();
    }
}
