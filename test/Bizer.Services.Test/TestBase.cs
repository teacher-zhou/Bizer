using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Test;
public abstract class TestBase
{
    private readonly ServiceProvider _builder;

    public TestBase()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _builder = services.BuildServiceProvider();
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    protected T GetService<T>() where T : notnull => _builder.GetRequiredService<T>();
}