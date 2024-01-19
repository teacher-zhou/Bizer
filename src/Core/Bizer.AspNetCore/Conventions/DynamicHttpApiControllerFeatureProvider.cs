using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Bizer.AspNetCore.Conventions;

/// <summary>
/// 表示自动化识别 HTTP API 的特性提供器。
/// </summary>
public class DynamicHttpApiControllerFeatureProvider : ControllerFeatureProvider
{
    /// <inheritdoc/>
    protected override bool IsController(TypeInfo typeInfo)
    {
        if ( !typeInfo.IsPublic && !typeInfo.IsClass && !typeInfo.IsGenericType && !typeInfo.IsAbstract )
        {
            return false;
        }

        if ( typeInfo.GetInterfaces().Any(m => m.IsDefined(typeof(ApiRouteAttribute))) )
        {
            return true;
        }
        return false;
    }
}
