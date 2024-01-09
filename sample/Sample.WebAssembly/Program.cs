using Sample.Services;
using Sample.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5192") });

//builder.Services.AddScoped(provider =>
//{
//    return (ITestService)BizerProxyGenerator.Create(typeof(ITestService), typeof(ProxyInterceptor));
//});


builder.Services.AddBizer(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddHttpClientConvension("http://localhost:5192");

var app = builder.Build();

await app.RunAsync();
