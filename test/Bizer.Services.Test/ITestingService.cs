namespace Bizer.Services.Test;

[ApiRoute("testing")]
public interface ITestingService
{
    [Get]
    Task<Returns> GetReturns();
}
