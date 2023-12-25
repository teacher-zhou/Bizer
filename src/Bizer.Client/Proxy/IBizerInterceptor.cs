using System.Reflection;

namespace Bizer.Client.Proxy;

public interface IBizerInterceptor
{
    object Intercept(MethodInfo method, object[] parameters);
}