using Project1.Domain.ValueObjects;

namespace Project1.Domain.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public string ProductName { get; private set; } = null!;
    public Money UnitPrice { get; private set; } = null!;

    public Money Total => Money.Create(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    private OrderItem()
    { }

    public static OrderItem Create(Guid productId, string productName, int quantity, Money price)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Product name is required.");

        return new OrderItem
        {
            ProductId = productId,
            ProductName = productName,
            Quantity = quantity,
            UnitPrice = price
        };
    }

    public void IncreaseQuantity(int additionalQuantity)
    {
        if (additionalQuantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(additionalQuantity));

        Quantity += additionalQuantity;
    }
}
