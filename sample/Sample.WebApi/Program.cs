using Sample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizer()
    .AddAutoDiscovery(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddBizerOpenApi();

var app = builder.Build();

app.UseRouting();

app.UseBizerOpenApi();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
