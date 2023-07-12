

using Microsoft.Extensions.DependencyInjection;
using Sample.Services;

var services = new ServiceCollection();
//services.AddScoped<IParameterService>
services.AddBizerClient(configure =>
{
    configure.BaseAddress = new("http://localhost:5192");
    configure.Add(typeof(IParameterService).Assembly);
});
var app = services.BuildServiceProvider();


var parameterService = app.GetRequiredService<IParameterService>();

await parameterService.Get("name");


