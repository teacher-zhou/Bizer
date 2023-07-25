using Bizer.Services;
using Microsoft.EntityFrameworkCore;
using Sample.Services;
using Sample.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizer(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddApiConvension().AddMapper().AddServiceInjection();

builder.Services.AddDbContext<TestDbContext>(options => options.UseInMemoryDatabase("db"));

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseBizerOpenApi();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
