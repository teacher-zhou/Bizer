using Bizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Services;

namespace Sample.WebApi
{
    public class TestService : ITestService
    {
        [Authorize]
        public Task Auth()
        {
            throw new NotImplementedException();
        }

        [ProducesResponseType(200,Type=typeof(Returns))]
        public Task<Returns> GetAsync()
        {
            return Returns.Success().ToResultTask();
        }

        [ProducesResponseType(200, Type = typeof(Returns))]
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

        [ProducesResponseType(200, Type = typeof(Returns<string>))]
        public Task<Returns<string>> GetHasData()
        {
            return Returns<string>.Success("返回值").ToResultTask();
        }

        public Task<string> PostAsync(string data)
        {
            return "一般返回值".ToResultTask();
        }

        public Task PostNothingAsync()
        {
            return Task.CompletedTask;
        }
        public Returns<string> PutData(string name)
        {
            return Returns<string>.Success(name);
        }

        public void PutNothing(int? id)
        {
        }
    }
}
