//using System.Net.Http;
//using System.Reflection;

//namespace Bizer.AspNetCore;

///// <summary>
///// Api 转换器。
///// </summary>
//internal class ApiConverter :RemotingConverter
//{
//    private readonly BizerApiOptions _apiOptions;

//    public ApiConverter(BizerApiOptions apiOptions)
//    {
//        _apiOptions = apiOptions;
//    }

//    public bool CanApiExplore(MethodInfo method)
//    {
//        return method is not null && method.TryGetCustomAttribute<HttpMethodAttribute>(out _);
//    }

//    public string GetApiRoute(Type interfaceType, MethodInfo method)
//    {
//        if ( interfaceType is null )
//        {
//            throw new ArgumentNullException(nameof(interfaceType));
//        }

//        var routeAppender = new List<string?>();

//        if ( interfaceType.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute) )
//        {
//            routeAppender.Add(routeAttribute!.Template);
//        }

//        method.TryGetCustomAttribute<HttpMethodAttribute>(out var http)

//        routeAppender.Add(httpMethodAttribute!.Template);

//        return string.Join("/", routeAppender);
//    }

//    public HttpMethod GetHttpMethod(string actionName, MethodInfo method)
//    {
//        if(method.TryGetCustomAttribute<HttpMethodAttribute>(out var httpMethodAttribute) )
//        {
//            return httpMethodAttribute.Method;
//        }

//        foreach ( var item in _apiOptions.HttpMethodSuffixMapping )
//        {
//            if ( actionName.StartsWith(item.Key) )
//            {
//                return item.Value;
//            }
//        }

//        throw new InvalidOperationException($"没有找到对应的 HttpMethod 方式，请在方法上定义{nameof(HttpMethodAttribute)}特性");
//    }

//    public Dictionary<string, (HttpParameterType type, string parameterName, object? parameterValue)> GetParameters(MethodInfo method)
//    {
//    }

//}
