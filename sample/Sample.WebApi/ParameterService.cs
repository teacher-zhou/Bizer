using Bizer;
using Sample.Services;

namespace Sample.WebApi
{
    public class ParameterService : IParameterService
    {
        public Task DeleteAsync([Path(null)] string id)
        {
            return Task.CompletedTask;
        }

        public Task Get(string name)
        {
            return Task.CompletedTask;
        }

        public Task GetByName(string name, string password)
        {
            return Task.CompletedTask;
        }

        public Task GetByNameValue(string name, string password, [Path(null)] string value)
        {
            return Task.CompletedTask;
        }

        public Task PostAsForm([Form(null)] string name, [Form(null)] string value)
        {
            return Task.CompletedTask;
        }

        public Task<int> PostAsync(PostModel model)
        {
            return Task.FromResult(1);
        }

        public Task<string> UpdateAsync([Path(null)] int value, PostModel model)
        {
            return Task.FromResult("success");
        }
    }
}
