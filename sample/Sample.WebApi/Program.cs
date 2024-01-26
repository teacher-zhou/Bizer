using Bizer;

using Sample.Contracts;
using Sample.Contracts.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddBizer(options => options.AddAssmebly(typeof(TestSerivce).Assembly)) //接口实现的程序集
    .AddDynamicWebApi()
    ;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAnyCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
