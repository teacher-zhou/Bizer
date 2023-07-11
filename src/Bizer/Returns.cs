namespace Bizer;

/// <summary>
/// 表示输出返回结果。
/// </summary>
[Serializable]
public record class Returns
{
    /// <summary>
    /// 初始化 <see cref="Returns"/> 类的新实例。
    /// </summary>
    /// <param name="errors">错误集合。</param>
    public Returns(IEnumerable<string?>? errors) => Errors = errors ?? Array.Empty<string?>();


    /// <summary>
    /// 获取返回的错误信息数组。
    /// </summary>
    public IEnumerable<string?> Errors { get; private set; } = Array.Empty<string?>();

    /// <summary>
    /// 获取一个布尔值，表示返回结果是否成功。
    /// </summary>
    public virtual bool Succeed => !Errors.Any();

    /// <summary>
    /// 表示操作结果是成功的。
    /// </summary>
    public static Returns Success() => new(Array.Empty<string?>());
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Returns Failed(params string[] errors) => new(errors);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static Returns Failed(IEnumerable<string?>? errors) => new(errors);

    /// <summary>
    /// 表示操作结果是成功的，并返回指定的结果。
    /// </summary>
    /// <typeparam name="TResult">结果类型。</typeparam>
    /// <param name="result">要返回的结果。</param>
    /// <returns>具备指定结果类型的 <see cref="Returns{TResult}"/> 实例。</returns>
    public static Returns<TResult> Success<TResult>(TResult result) => new(result, Array.Empty<string>());
}

/// <summary>
/// 表示具有返回值 <typeparamref name="TResult"/> 的输出返回结果。
/// </summary>
/// <typeparam name="TResult">返回值的类型。</typeparam>
[Serializable]
public record class Returns<TResult> : Returns
{
    /// <summary>
    /// 初始化 <see cref="Returns{TResult}"/> 类的新实例。
    /// </summary>
    /// <param name="data">要返回的数据。</param>
    /// <param name="errors">错误信息数组。</param>
    public Returns(TResult? data, IEnumerable<string>? errors) : base(errors) => Data = data;

    /// <summary>
    /// 获取执行结果成功后的返回数据。
    /// </summary>
    public TResult? Data { get; } = default;

    /// <summary>
    /// 表示操作结果是成功的，并设置返回的数据。
    /// </summary>
    public static Returns<TResult> Success(TResult data) => new(data, null);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Returns<TResult> Failed(params string[] errors) => new(default, errors);
    /// <summary>
    /// 表示操作结果是失败的。
    /// </summary>
    /// <param name="errors">操作失败的错误信息数组。</param>
    public static new Returns<TResult> Failed(IEnumerable<string>? errors) => new(default, errors);
}
