using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bizer.Test;
public abstract class TestBase
{
    private readonly IServiceProvider _builder;

    public TestBase()
    {
        _builder = Host.CreateDefaultBuilder().ConfigureServices(ConfigureServices).Build().Services;
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    protected IServiceProvider ServiceProvider => _builder;

    protected T? GetService<T>() where T : class => _builder.GetService<T>();
}