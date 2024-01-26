using Bizer;

namespace Sample.Contracts;

[ApiRoute("api/test", Name = "Users")]
public interface ITestService
{
    [Get("{id}")]
    Task<string> GetWithId([Route] int id);

    [Get(Name = "Query")]
    Task<Returns> GetFromQueryParameter(string name);

    [Post(Name = "asign")]
    Task<Returns<User>> SignInAsync([Body] User model);

    [Get("weather")]
    Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
}

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
}


public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}