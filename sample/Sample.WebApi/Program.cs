using Sample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizerOpenApi(config =>
{
    config.Add(typeof(IAccountService).Assembly);
});
var app = builder.Build();

app.UseRouting();

app.UseBizerOpenApi();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
