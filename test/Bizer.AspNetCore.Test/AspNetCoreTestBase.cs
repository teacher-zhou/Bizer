using Bizer.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bizer.AspNetCore.Test;
public class AspNetCoreTestBase:TestBase,IDisposable
{
    private IWebHost _host;

    public AspNetCoreTestBase()
    {
        var builder = new WebHostBuilder().UseTestServer().ConfigureServices(services =>
        {
            services.AddBizer(options=>options.Assemblies.Add(typeof(ITestingService).Assembly))
                    .AddOpenApiConvension();
            ConfigureServices(services);
        });

        _host = builder.Start();
    }

    protected HttpClient GetClient() => _host.GetTestClient();

    public void Dispose()
    {
        _host.Dispose();
    }
}
