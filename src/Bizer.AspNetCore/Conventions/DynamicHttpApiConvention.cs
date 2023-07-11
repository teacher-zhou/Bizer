using Bizer.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;

namespace Bizer.AspNetCore.Conventions;

/// <summary>
/// 动态 HTTP API 的约定。
/// </summary>
internal class DynamicHttpApiConvention : IApplicationModelConvention
{
    /// <summary>
    /// controller 后缀关键字。
    /// </summary>
    readonly static string[] _controller_Suffix_Keywords = new string[] { "BusinessService", "AppService", "BusinessService", "BizService", "Service" };
    /// <summary>
    /// 识别并替换的 action 前缀关键字。
    /// </summary>
    readonly static string[] _action_Prefix_Keywords = new[] { "Get", "Post", "Create", "Add", "Insert", "Put", "Update", "Patch", "Delete", "Remove" };
    readonly static Dictionary<string, string> _action_HttpMethod_Mapping = new()
    {
        ["Create"] = HttpMethods.Post,
        ["Add"] = HttpMethods.Post,
        ["Insert"] = HttpMethods.Post,
        ["Update"] = HttpMethods.Put,
        ["Edit"] = HttpMethods.Put,
        ["Patch"] = HttpMethods.Put,
        ["Delete"] = HttpMethods.Delete,
        ["Remove"] = HttpMethods.Delete,
        ["Get"] = HttpMethods.Get,
        ["Find"] = HttpMethods.Get,
    };
    private Type? _interfaceAsControllerType;

    /// <inheritdoc/>
    public void Apply(ApplicationModel application)
    {
        foreach ( var controller in application.Controllers )
        {
            _interfaceAsControllerType = GetOnlyServiceType(controller.ControllerType);
            if ( _interfaceAsControllerType is null )
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

        if ( _interfaceAsControllerType!.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute) && !string.IsNullOrEmpty(routeAttribute!.Name) )
        {
            controller.ControllerName = routeAttribute.Name;
        }

        foreach ( var action in controller.Actions )
        {
            if ( action is null )
            {
                continue;
            }

            var actionMethodAttribute = FindHttpMethodFromAction(action);
            if ( actionMethodAttribute is null )
            {
                action.ApiExplorer.IsVisible = false;
                continue;
            }
            action.ApiExplorer.IsVisible = true;


        }
    }

    private void ConfigureSelector(ControllerModel controller)
    {
        RemoveEmptySelectors(controller.Selectors);

        if ( controller.Selectors.Any(temp => temp.AttributeRouteModel != null) )
        {
            return;
        }

        foreach ( var action in controller.Actions )
        {
            ConfigureSelector(action);
        }

        void ConfigureSelector(ActionModel action)
        {
            if ( FindHttpMethodFromAction(action) is null )
            {
                return;
            }

            RemoveEmptySelectors(action.Selectors);

            if ( action.Selectors.Count <= 0 )
            {
                AddBusinessServiceSelector(action);
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
        foreach ( var action in controller.Actions )
        {
            var method = FindInterfaceMethod(action);
            if ( method is null )
            {
                continue;
            }

            foreach ( var parameter in action.Parameters )
            {
                if ( parameter.BindingInfo != null )
                {
                    continue;
                }

                var methodParameter = method.GetParameters().SingleOrDefault(m => m.Name == parameter.Name);

                if ( methodParameter is null )
                {
                    continue;
                }

                var httpParameterAttribute = methodParameter.GetCustomAttribute<HttpParameterAttribute>();
                if ( httpParameterAttribute is null )
                {
                    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() { Name = methodParameter.Name } });
                    continue;
                }
                parameter.BindingInfo = httpParameterAttribute.Type switch
                {
                    HttpParameterType.FromBody => BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() }),
                    HttpParameterType.FromForm => BindingInfo.GetBindingInfo(new[] { new FromFormAttribute() { Name = httpParameterAttribute.Name } }),
                    HttpParameterType.FromHeader => BindingInfo.GetBindingInfo(new[] { new FromHeaderAttribute() { Name = httpParameterAttribute.Name } }),
                    HttpParameterType.FromPath => BindingInfo.GetBindingInfo(new[] { new FromRouteAttribute() { Name = httpParameterAttribute.Name } }),
                    _ => BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() { Name = httpParameterAttribute.Name } }),
                };
            }
        }
    }

    void AddBusinessServiceSelector(ActionModel action)
    {
        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(GenerateRoute(action))
        };
        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));

        action.Selectors.Add(selector);
    }
    void NormalizeSelectorRoutes(ActionModel action)
    {
        foreach ( var selector in action.Selectors )
        {
            if ( selector.AttributeRouteModel == null )
            {
                selector.AttributeRouteModel = new AttributeRouteModel(GenerateRoute(action));
            }

            if ( !selector.ActionConstraints.Any() )
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
    RouteAttribute GenerateRoute(ActionModel action)
    {
        if ( _interfaceAsControllerType is null )
        {
            throw new InvalidOperationException($"不能识别成 Controller");
        }

        var routeAppender = new List<string>();
        if ( _interfaceAsControllerType.TryGetCustomAttribute<ApiRouteAttribute>(out var routeAttribute) )
        {
            routeAppender.Add(routeAttribute!.Template);
        }
        else
        {
            var name = _interfaceAsControllerType.Name;

            _controller_Suffix_Keywords.Where(suffix => name.EndsWith(suffix))
                .ForEach(text =>
                {
                    name = name.Replace(text, "");
                });
            routeAppender.Add(name);
        }

        #region 方法的路由

        var httpMethodAttribute = FindHttpMethodFromAction(action);
        if ( string.IsNullOrWhiteSpace(httpMethodAttribute!.Template) )
        {
            var actionName = action.ActionName;

            if ( actionName.EndsWith("Async") )
            {
                actionName = actionName.Replace("Async", string.Empty);
            }

            foreach ( var trimPrefix in _action_Prefix_Keywords.Where(trimPrefix => actionName.StartsWith(trimPrefix)) )
            {
                actionName = actionName[trimPrefix.Length..];
                break;
            }
            routeAppender.Add(actionName);
        }
        else
        {
            routeAppender.Add(httpMethodAttribute!.Template);
        }
        #endregion


        return new(string.Join("/", routeAppender))
        {
            Name = routeAttribute?.Name ?? httpMethodAttribute?.Name ?? action.ActionName,
            Order = routeAttribute?.Order ?? 0
        };
    }

    /// <summary>
    /// 从方法的接口处识别出 <see cref="HttpMethodAttribute"/> 特性。没有该特性的方法将不作为 api 检测范围
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private HttpMethodAttribute? FindHttpMethodFromAction(ActionModel action)
    {
        MethodInfo? actionMethod = FindInterfaceMethod(action);

        if ( actionMethod is null )
        {
            //Action，有这个方法，但接口没有这个方法，会报错。
            //throw new InvalidOperationException($"找不到 {action.ActionName} 方法");

            return default;
        }

        return actionMethod.GetCustomAttributes<HttpMethodAttribute>().OrderBy(m => m.Order).FirstOrDefault();
    }

    /// <summary>
    /// 从 action 中识别出符合接口的方法。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private MethodInfo? FindInterfaceMethod(ActionModel action)
    {
        var allmethods = _interfaceAsControllerType!.GetMethods().Concat(_interfaceAsControllerType.GetInterfaces().SelectMany(m => m.GetMethods()));

        var methodName = action.ActionMethod.Name;

        var actionMethod = allmethods.SingleOrDefault(m => m.Name == methodName);
        return actionMethod;
    }

    /// <summary>
    /// 获取 HTTP Method 字符串。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    string GetHttpMethod(ActionModel action)
    {
        var httpMethodAttribute = FindHttpMethodFromAction(action);
        if ( httpMethodAttribute is null )
        {
            if ( _action_HttpMethod_Mapping.TryGetValue(action.ActionName, out var mappingMethodName) )
            {
                return mappingMethodName;
            }
            return HttpMethods.Get;
        }
        return httpMethodAttribute.Method.Method;
    }


    private static Type? GetOnlyServiceType(TypeInfo controller)
    {
        return controller.GetInterfaces().FirstOrDefault(remoteInterfaceType => remoteInterfaceType.TryGetCustomAttribute<ApiRouteAttribute>(out var serviceAttribute));
    }

}
