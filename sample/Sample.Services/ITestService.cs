using Bizer;

namespace Sample.Services;
[ApiRoute("api/test")]
public interface ITestService
{
    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    [Get]
    Task<Returns> GetAsync();

    [Get("no")]
    Returns No();

    [Get("{id}")]
    Task<Returns> GetFromPathAsync([Route] int id);
    [Get("query")]
    Task<Returns> GetFromQueryAsync([Query] string? name);

    [Get("header")]
    Task<Returns> GetFromHeaderAsync([Header("Authorization")] string? header);

    [Get("data")]
    Task<Returns<string>> GetHasData();

    [Post]
    Task<string> PostAsync(string data);

    [Post("no-return")]
    Task PostNothingAsync();

    [Put("{name}")]
    Returns<string> PutData([Route] string name);

    [Put("no-return")]
    void PutNothing(int? id);
    [Post("auth")]
    Task Auth();

    [Get("search")]
    Task<Returns> Search([Query] SearchFilter model);
}


public record SearchFilter
{
    public string? Title { get; set; }
}