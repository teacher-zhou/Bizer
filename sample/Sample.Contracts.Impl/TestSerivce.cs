using Bizer;

namespace Sample.Contracts.Impl;

public class TestSerivce : ITestService
{
    public Task<Returns> GetFromQueryParameter(string name)
    {
        return Returns.Success().AsTask();
    }

    public Task<IEnumerable<WeatherForecast>> GetWeatherAsync()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).AsTask();
    }

    public Task<string> GetWithId([Route] int id)
    {
        return $"{id}".AsTask();
    }

    public Task<Returns<User>> SignInAsync([Body] User model)
    {
        return Returns<User>.Success(model).WithMessage("成功").AsTask();
    }
}
