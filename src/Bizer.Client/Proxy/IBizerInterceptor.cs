using System.Reflection;

namespace Bizer.Client.Proxy;

public interface IBizerInterceptor
{
    Task<object> Intercept(MethodInfo method, object[] parameters);
}