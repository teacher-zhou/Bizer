using Microsoft.AspNetCore.ResponseCompression;

using Sample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddBizer(configure => configure.Assemblies.Add(typeof(ITestService).Assembly)).AddOpenApiConvension();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.UseBizerOpenApi();
app.MapFallbackToFile("index.html");

app.Run();
