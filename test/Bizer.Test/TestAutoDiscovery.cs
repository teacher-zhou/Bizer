using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizer.Test;
public class TestAutoDiscovery:TestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBizer(m => m.AssembyNames.Add("Bizer.Test"));
    }

    [Fact]
    public void Test_AutoDiscovery()
    {
        var options=GetService<AutoDiscoveryOptions>();

        var assemblies= options.GetDiscoveredAssemblies();
        Assert.NotEmpty(assemblies);

        Assert.Contains(typeof(TestAutoDiscovery).Assembly.FullName, assemblies.Select(m=>m.FullName));
    }
}
