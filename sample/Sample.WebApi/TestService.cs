using Bizer;
using Sample.Services;

namespace Sample.WebApi
{
    public class TestService : ITestService
    {
        public Task<Returns> GetAsync()
        {
            return Returns.Success().SetCode("123456").ToResultTask();
        }

        public Task<Returns> GetFromHeaderAsync(string? header)
        {
            return Returns.Success().ToResultTask();
        }

        public Task<Returns> GetFromPathAsync(int id)
        {
            return Returns.Success().ToResultTask();
        }

        public Task<Returns> GetFromQueryAsync([Query(null)] string? name)
        {
            return Returns.Success().ToResultTask();
        }

        public Task<Returns<string>> GetHasData()
        {
            return Returns<string>.Success("返回值").ToResultTask();
        }
    }
}
