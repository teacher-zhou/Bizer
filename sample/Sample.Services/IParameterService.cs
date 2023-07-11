using Bizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services;
[ApiRoute("api/parameter")]
public interface IParameterService
{
    [Get]
    Task Get(string name);

    [Get]
    Task GetByName(string name, string password);

    [Get("{value}")]
    Task GetByNameValue(string name, string password, [Path]string value);

    [Post]
    Task<int> PostAsync(PostModel model);

    [Put("{id}")]
    Task<string> UpdateAsync([Path]int value,PostModel model);

    [Delete("{id}")]
    Task DeleteAsync([Path]string id);

    [Post]
    Task PostAsForm([Form] string name, [Form] string value);
}

public class PostModel
{
    public string Name { get; set; }
}