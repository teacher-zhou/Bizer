namespace Bizer.Services;

public interface IHasId<out TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}
