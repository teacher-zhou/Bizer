using Bizer.AspNetCore;
using Bizer.Services.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizer.AspNetCore.Test;
public class AspNetCoreTestBase:TestBase,IDisposable
{
    private IWebHost _host;

    public AspNetCoreTestBase()
    {
        var builder = new WebHostBuilder().UseTestServer().ConfigureServices(services =>
        {
            services.AddBizer()
                    .AddAutoDiscovery(options=>options.Assemblies.Add(typeof(ITestingService).Assembly))
                    .AddBizerOpenApi();
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
