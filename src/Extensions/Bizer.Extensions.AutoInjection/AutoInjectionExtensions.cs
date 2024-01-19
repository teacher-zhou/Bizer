using System.Reflection;

namespace Bizer;

public static class AutoInjectionExtensions
{
    /// <summary>
    /// 添加自动注入功能。
    /// 只有添加了特性 <see cref="InjectServiceAttribute"/> 的接口，会自动将实现类作为进行注册。
    /// </summary>
    /// <returns></returns>
    public static BizerBuilder AddServiceInjection(this BizerBuilder builder)
    {
        var allClassTypes = builder.AutoDiscovery.GetDiscoveredAssemblies().SelectMany(m => m.ExportedTypes).Where(m => m.IsClass && !m.IsAbstract)
           .Where(classType => classType.GetInterfaces().Any(interfaceType => interfaceType.IsDefined(typeof(InjectServiceAttribute))));

        foreach (var implementType in allClassTypes)
        {
            var serviceAttributeInterface = implementType.GetInterfaces().Where(interfaceType => interfaceType.IsDefined(typeof(InjectServiceAttribute))).Last();

            var serviceAttribute = serviceAttributeInterface.GetCustomAttribute<InjectServiceAttribute>();

            builder.Services.Add(new(serviceAttributeInterface, implementType, serviceAttribute!.Lifetime));
        }
        return builder;
    }
}
