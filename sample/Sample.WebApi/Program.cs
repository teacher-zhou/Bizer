using Bizer.Services;
using Microsoft.EntityFrameworkCore;
using Sample.Services;
using Sample.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizer(options => options.Assemblies.Add(typeof(ITestService).Assembly))
    .AddOpenApiConvension()
    .AddMapper()
    .AddServiceInjection()
    .AddHttpContextPricipalAccessor()
    .AddDbContext<TestDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=TestDb;Trusted_Connection=true",b=>b.MigrationsAssembly("Sample.WebApi")))
    ;

builder.Services.AddCors(options=>options.AddDefaultPolicy(b=>b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

//builder.Services.AddDbContext<TestDbContext>(options => options.UseInMemoryDatabase("db"));

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.UseRouting();
app.UseBizerOpenApi();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.Run();
