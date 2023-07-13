using Bizer;

namespace Sample.Services
{
    [ApiRoute("api/test")]
    public interface ITestService
    {
        [Get]
        Task<Returns> GetAsync();

        [Get("{id}")]
        Task<Returns> GetFromPathAsync([Path]int id);
        [Get("query")]
        Task<Returns> GetFromQueryAsync([Query]string? name);

        [Get("header")]
        Task<Returns> GetFromHeaderAsync([Header("Authorization")]string? header);

        [Get("data")]
        Task<Returns<string>> GetHasData();
    }
}
