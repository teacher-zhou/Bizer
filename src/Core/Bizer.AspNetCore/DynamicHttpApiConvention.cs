using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Reflection;

namespace Bizer.AspNetCore;

/// <summary>
/// 动态 HTTP API 的约定。
/// </summary>
internal class DynamicHttpApiConvention : IApplicationModelConvention
{
    private readonly IHttpRemotingResolver _converter;
    private Type? _interfaceAsControllerType;

    public DynamicHttpApiConvention(IHttpRemotingResolver converter)
    {
        _converter = converter;
    }

    /// <inheritdoc/>
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            _interfaceAsControllerType = GetOnlyServiceType(controller.ControllerType);
            if (_interfaceAsControllerType is null)
            {
                return;
            }

            ConfigureApiExplorer(controller);
            ConfigureSelector(controller);
            ConfigureParameters(controller);
        }
    }

    void ConfigureApiExplorer(ControllerModel controller)
    {
        controller.ApiExplorer.IsVisible = true;

        if (_interfaceAsControllerType!.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute) && !string.IsNullOrEmpty(routeAttribute!.Name))
        {
            controller.ControllerName = routeAttribute.Name;
        }

        foreach (var action in controller.Actions)
        {
            if (action is null)
            {
                continue;
            }

            var method = FindInterfaceMethodFromAction(action);

            action.ApiExplorer.IsVisible = _converter.CanApiExplore(method);
        }
    }

    private void ConfigureSelector(ControllerModel controller)
    {
        RemoveEmptySelectors(controller.Selectors);

        if (controller.Selectors.Any(temp => temp.AttributeRouteModel != null))
        {
            return;
        }

        foreach (var action in controller.Actions)
        {
            ConfigureSelector(action);
        }

        void ConfigureSelector(ActionModel action)
        {
            if (FindHttpMethodFromAction(action) is null)
            {
                return;
            }

            RemoveEmptySelectors(action.Selectors);

            if (action.Selectors.Count <= 0)
            {
                AddSelector(action);
            }
            else
            {
                NormalizeSelectorRoutes(action);
            }
        }

        static void RemoveEmptySelectors(IList<SelectorModel> selectors)
            => selectors.Where(selector => selector.AttributeRouteModel == null && !selector.ActionConstraints.Any() && !selector.EndpointMetadata.Any())
                .ToList().ForEach(s => selectors.Remove(s));
    }

    private void ConfigureParameters(ControllerModel controller)
    {
        foreach (var action in controller.Actions)
        {
            var method = FindInterfaceMethodFromAction(action);
            if (method is null)
            {
                continue;
            }

            var parameters = _converter.GetParameters(method);

            if (!parameters.TryGetValue(DefaultHttpRemotingResolver.GetMethodCacheKey(method), out var parameterList))
            {
                continue;
            }

            foreach (var actionParameter in action.Parameters)
            {
                if (actionParameter.BindingInfo != null)
                {
                    continue;
                }

                var parameterInfo = parameterList.Single(m => m.Name == actionParameter.Name);

                var parameterName = parameterInfo.GetParameterNameInHttpRequest();

                actionParameter.BindingInfo = parameterInfo.Type switch
                {
                    HttpParameterType.FromBody => BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() }),
                    HttpParameterType.FromForm => BindingInfo.GetBindingInfo(new[] { new FromFormAttribute() { Name = parameterName } }),
                    HttpParameterType.FromHeader => BindingInfo.GetBindingInfo(new[] { new FromHeaderAttribute() { Name = parameterName } }),
                    HttpParameterType.FromQuery => BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() { Name = parameterName } }),
                    _ => BindingInfo.GetBindingInfo(new[] { new FromRouteAttribute() { Name = parameterName } }),
                };
            }
        }
    }

    void AddSelector(ActionModel action)
    {
        var route = GenerateRoute(action);
        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(route)
        };
        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));

        action.Selectors.Add(selector);
    }
    void NormalizeSelectorRoutes(ActionModel action)
    {
        foreach (var selector in action.Selectors)
        {
            var route = GenerateRoute(action);

            selector.AttributeRouteModel ??= new AttributeRouteModel(route);

            if (!selector.ActionConstraints.Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
            }
        }
    }

    /// <summary>
    /// 生成路由。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Microsoft.AspNetCore.Mvc.RouteAttribute GenerateRoute(ActionModel action)
    {
        _interfaceAsControllerType!.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute);

        var method = FindInterfaceMethodFromAction(action);

        var actionAttribute = FindHttpMethodFromAction(action);

        var routeTemplate = _converter.GetApiRoute(_interfaceAsControllerType!, method);

        return new(routeTemplate)
        {
            Name = $"{routeAttribute?.Name ?? action.Controller.ControllerName}-{actionAttribute?.Name ?? action.ActionName}",
            Order = routeAttribute?.Order ?? 1000
        };
    }

    /// <summary>
    /// 从方法的接口处识别出 <see cref="HttpMethodAttribute"/> 特性。没有该特性的方法将不作为 api 检测范围
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private HttpMethodAttribute? FindHttpMethodFromAction(ActionModel action)
    {
        MethodInfo? actionMethod = FindInterfaceMethodFromAction(action);

        if (actionMethod is null)
        {
            //Action，有这个方法，但接口没有这个方法，会报错。
            //throw new InvalidOperationException($"找不到 {action.ActionName} 方法");

            return default;
        }

        return actionMethod.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault();
    }

    /// <summary>
    /// 从 action 中识别出符合接口的方法。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private MethodInfo FindInterfaceMethodFromAction(ActionModel action)
    {
        var allmethods = _interfaceAsControllerType!.GetMethods().Concat(_interfaceAsControllerType.GetInterfaces().SelectMany(m => m.GetMethods()));

        var methodName = action.ActionName;

        //同名方法的重载必须唯一

        var filterMethods = allmethods.Where(t => t.Name == action.ActionMethod.Name && t.GetParameters().Count() == action.Parameters.Count);
        if (!filterMethods.Any())
        {
            throw new InvalidOperationException($"没有在接口'{_interfaceAsControllerType.Name}'找到方法'{action.ActionName}'");
        }
        if (filterMethods.Count() > 1)
        {
            throw new InvalidOperationException($"在'{_interfaceAsControllerType.Name}'找到多个方法'{action.ActionName}'并且参数都是 {filterMethods.SelectMany(m => m.GetParameters()).Select(t => $"{t.Name}:{t.ParameterType.Name}").Aggregate((prev, next) => $"{prev},{next}")}");
        }

        var onlyMethod = filterMethods.Single();

        var methodKey = DefaultHttpRemotingResolver.GetMethodCacheKey(onlyMethod);

        var actionMethod = allmethods.SingleOrDefault(m => DefaultHttpRemotingResolver.GetMethodCacheKey(m) == methodKey);

        if (actionMethod is null)
        {
            throw new InvalidOperationException($"没有找到方法 '{nameof(methodKey)}:{methodKey}'");
        }

        return actionMethod;
    }

    /// <summary>
    /// 获取 HTTP Method 字符串。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    string GetHttpMethod(ActionModel action)
    {
        //var httpMethodAttribute = FindHttpMethodFromAction(action);
        var method = FindInterfaceMethodFromAction(action);
        var httpMethod = _converter.GetHttpMethod(method);

        return httpMethod.Method;
    }


    private static Type? GetOnlyServiceType(TypeInfo controller)
    {
        return controller.GetInterfaces().FirstOrDefault(remoteInterfaceType => remoteInterfaceType.TryGetCustomAttribute<ApiRouteAttribute>(out var serviceAttribute));
    }
}