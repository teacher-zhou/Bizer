using Microsoft.Extensions.DependencyInjection;

namespace Bizer.Test;
public interface IModularService1
{

}

class ModularService1 : IModularService1 { }

public class TestAppModule : AppModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IModularService1, ModularService1>();
        services.AddTransient<ModularService1>();
    }

    public override void ConfigureBizer(BizerBuilder builder)
    {
        //builder.Services.AddScoped<IModularService1, ModularService1>();
    }
}

//[BaseOn<TestAppModule>]
//public class BaseOnTestAppModule:AppModule
//{

//}

public class TestModular:TestBase
{

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBizer(m => m.AssembyNames.Add("Bizer.Test")).AddModular();

        //services.AddTransient<IModularService1, ModularService1>();

    }

    [Fact]
    public void Test_Not_Null_Service()
    {
        //using var scope=ServiceProvider.CreateScope();
        var service = ServiceProvider.GetService<ModularService1>();

        Assert.NotNull(service);
    }
}
