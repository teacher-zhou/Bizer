namespace Bizer;

/// <summary>
/// 表示输出返回结果。
/// </summary>
[Serializable]
public class Returns
{
    /// <summary>
    /// 初始化 <see cref="Returns"/> 类的新实例。
    /// </summary>
    /// <param name="messages">错误集合。</param>
    /// <param name="code">代码。</param>
    public Returns(bool succeed = default, IEnumerable<string>? messages = default)
    {
        _messageCollection.AddRange(messages ?? Array.Empty<string>());
        Succeed = succeed;
    }

     List<string> _messageCollection = new();

    /// <summary>
    /// 获取返回的信息数组。
    /// </summary>
    public IEnumerable<string?> Messages => _messageCollection;

    /// <summary>
    /// 获取一个布尔值，表示是否成功的结果。
    /// </summary>
    public bool Succeed { get; protected set; }

    /// <summary>
    /// 获取自定义代码。
    /// </summary>
    public string? Code { get; internal set; }

    /// <summary>
    /// 表示操作结果是成功的。
    /// </summary>
    public static Returns Success() => new(true);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Returns Failed(params string[] errors) => new(messages: errors);
}

/// <summary>
/// 表示具有返回值 <typeparamref name="TResult"/> 的输出返回结果。
/// </summary>
/// <typeparam name="TResult">返回值的类型。</typeparam>
[Serializable]
public class Returns<TResult> : Returns
{
    /// <summary>
    /// 初始化 <see cref="Returns{TResult}"/> 类的新实例。
    /// </summary>
    public Returns(bool succeed = default, IEnumerable<string>? messages = default, TResult? data = default) : base(succeed, messages)
    {
        Data = data;
    }

    /// <summary>
    /// 获取执行结果成功后的返回数据。
    /// </summary>
    public TResult? Data { get; private set; } = default;

    /// <summary>
    /// 表示操作结果是成功的，并设置返回的数据。
    /// </summary>
    public static Returns<TResult> Success(TResult? data = default) => new(true, data: data);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Returns<TResult> Failed(params string[] errors) => new(messages: errors);
}
