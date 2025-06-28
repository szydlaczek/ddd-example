using Project1.Domain.Exceptions;
using Project1.Domain.ValueObjects;

namespace Project1.Domain.Entities;

public class Order : AggregateRoot<Guid>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public Money TotalAmount => Money.Create(_items.Sum(i => i.Total.Amount), _items.First().Total.Currency);

    private Order()
    { }

    public static Order Create(IEnumerable<(Guid productId, string productName, int quantity, Money unitPrice)> items)
    {
        if (!items.Any())
            throw new InvalidOperationException("Order must contain at least one item.");

        var currency = items.First().unitPrice.Currency;

        if (items.Any(i => i.unitPrice.Currency != currency))
            throw new InvalidOperationException("All items must use the same currency.");

        var order = new Order();

        order.Id = SequentialGuid.SequentialGuidGenerator.Instance.NewGuid();

        foreach (var item in items)
        {
            order._items.Add(OrderItem.Create(item.productId, item.productName, item.quantity, item.unitPrice));
        }

        return order;
    }

    public void Submit()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOrderStatusException($"Cannot submit order in status {Status}");

        Status = OrderStatus.Submitted;
        //  AddDomainEvent(new OrderSubmittedEvent(this.Id));
    }

    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Only draft orders can be modified.");

        if (unitPrice.Currency != _items.First().UnitPrice.Currency)
            throw new InvalidOperationException("Cannot mix currencies in one order.");

        var existing = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existing != null)
        {
            existing.IncreaseQuantity(quantity);
        }
        else
        {
            _items.Add(OrderItem.Create(productId, productName, quantity, unitPrice));
        }
    }
}
