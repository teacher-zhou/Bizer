using Bizer;
using Bizer.Services;

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
    Task<Returns> GetFromPathAsync([Path] int id);
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
    Returns<string> PutData([Path] string name);

    [Put("no-return")]
    void PutNothing(int? id);
    [Post("auth")]
    Task Auth();

    [Get("search")]
    Task<Returns> Search([Query] SearchFilter model);
}


public record SearchFilter(int Page, int Size) : PagedDto(Page, Size)
{
    public SearchFilter() : this(1, 10)
    {

    }

    public string? Title { get; set; }
}