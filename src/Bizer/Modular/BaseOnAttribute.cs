namespace Bizer;

/// <summary>
/// 依赖指定的模块类型。
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class BaseOnAttribute : Attribute
{
    /// <summary>
    /// 使用指定的模块类型初始化 <see cref="BaseOnAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="moduleType">模块类型。该类型必须派生自 <see cref="AppModule"/> 类。</param>
    public BaseOnAttribute(Type moduleType)
    {
        if ( !typeof(AppModule).IsAssignableFrom(moduleType) )
        {
            throw new InvalidOperationException($"指定的模块'{moduleType.Name}'必须要从'{nameof(AppModule)}'派生");
        }

        ModuleType = moduleType;
    }

    /// <summary>
    /// 获取模块的类型。
    /// </summary>
    public Type ModuleType { get; }
}

#if NET7_0_OR_GREATER

/// <summary>
/// 依赖指定的模块类型。
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class BaseOnAttribute<TModule> : BaseOnAttribute where TModule : AppModule
{
    /// <summary>
    /// 初始化 <see cref="BaseOnAttribute{TModule}"/> 的新实例。
    /// </summary>
    public BaseOnAttribute() : base(typeof(TModule))
    {
    }
}
#endif
