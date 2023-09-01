using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Sample.Services;
using Sample.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddBizer(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddHttpClientConvension("http://localhost:5192");

var app = builder.Build();

await app.RunAsync();
