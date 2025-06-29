namespace Project1.Domain.Common;

public abstract class Entity<T>
{
    public T Id { get; protected set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T> other) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id!.Equals(other.Id);
    }

    public override int GetHashCode() => Id!.GetHashCode();
}
