using Bizer;
using Bizer.Services;

using Microsoft.EntityFrameworkCore;

using Sample.Services;
using Sample.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBizer(options => options.AddAssmebly(typeof(ITestService).Assembly))
    .AddDynamicWebApi()
    .AddMapper()
    .AddHttpContextPricipalAccessor()
    .AddDbContext<TestDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=Test;Trusted_Connection=true", b => b.MigrationsAssembly("Sample.WebApi")))
    ;

builder.Services.AddCors(options => options.AddDefaultPolicy(b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

//builder.Services.AddDbContext<TestDbContext>(options => options.UseInMemoryDatabase("db"));

var app = builder.Build().WithBizer();

app.UseDeveloperExceptionPage();
app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.UseRouting();
app.UseBizer(options => options.EnableRedocPath = true);

app.Run();
