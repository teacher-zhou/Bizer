using Bizer;

namespace Sample.Services;
[ApiRoute("api/parameter")]
public interface IParameterService
{
    [Get("name")]
    Task Get(string name);

    [Get("by-name")]
    Task GetByName(string name, string password);

    [Get("by-name/{value}")]
    Task GetByNameValue(string name, string password, [Path]string value);

    [Post()]
    Task<int> PostAsync(PostModel model);

    [Put("{id}")]
    Task<string> UpdateAsync([Path]int value,PostModel model);

    [Delete("{id}")]
    Task DeleteAsync([Path]string id);

    [Post("as-form")]
    Task PostAsForm([Form] string name, [Form] string value);
}

public class PostModel
{
    public string Name { get; set; }
}