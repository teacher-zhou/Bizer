using Bizer;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Sample.Blazor.Client;
using Sample.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBizer(options => options.AddAssmebly(typeof(ITestService).Assembly))
    .AddDynamicHttpProxy("http://localhost:5216/");

builder.Services.AddScoped<AuthenticationStateProvider, SampleAuthenticationStateProvider>();
builder.Services.AddScoped<SampleAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
