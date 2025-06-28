namespace Project1.Domain.ValueObjects;

public class OrderStatus : ValueObject
{
    public string Value { get; }

    private OrderStatus(string value)
    {
        Value = value;
    }

    public static readonly OrderStatus Draft = new("Draft");
    public static readonly OrderStatus Submitted = new("Submitted");
    public static readonly OrderStatus Paid = new("Paid");
    public static readonly OrderStatus Cancelled = new("Cancelled");

    public static IEnumerable<OrderStatus> All => new[] { Draft, Submitted, Paid, Cancelled };

    public static OrderStatus From(string value)
    {
        var status = All.FirstOrDefault(s => s.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        if (status is null)
            throw new InvalidOperationException($"Unknown OrderStatus: '{value}'");

        return status;
    }

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool operator ==(OrderStatus left, OrderStatus right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(OrderStatus left, OrderStatus right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        return ((OrderStatus)obj).Value.Equals(Value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Value.ToLowerInvariant().GetHashCode();
    }
}
