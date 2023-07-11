using Bizer;
using Sample.Services;

namespace Sample.WebApi
{
    public class ParameterService : IParameterService
    {
        public Task DeleteAsync([Path(null)] string id)
        {
            throw new NotImplementedException();
        }

        public Task Get(string name)
        {
            throw new NotImplementedException();
        }

        public Task GetByName(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task GetByNameValue(string name, string password, [Path(null)] string value)
        {
            throw new NotImplementedException();
        }

        public Task PostAsForm([Form(null)] string name, [Form(null)] string value)
        {
            throw new NotImplementedException();
        }

        public Task<int> PostAsync(PostModel model)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync([Path(null)] int value, PostModel model)
        {
            throw new NotImplementedException();
        }
    }
}
