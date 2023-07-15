using Sample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizer(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddApiConvension();

var app = builder.Build();

app.UseRouting();

app.UseBizerOpenApi();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
