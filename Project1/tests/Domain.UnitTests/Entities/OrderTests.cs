using NUnit.Framework;
using Project1.Domain.Entities;
using Project1.Domain.Exceptions;
using Project1.Domain.ValueObjects;

namespace Project1.Domain.UnitTests.Entities;

[TestFixture]
public class OrderTests
{
    private readonly Money _money = Money.Create(10m, "PLN");

    [Test]
    public void Create_WithEmptyItemList_Throws()
    {
        // Arrange
        var items = new List<(Guid, string, int, Money)>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Order.Create(items));
    }

    [Test]
    public void Create_WithMixedCurrencies_Throws()
    {
        // Arrange
        var items = new List<(Guid, string, int, Money)>
        {
            (Guid.NewGuid(), "Item 1", 1, Money.Create(10m, "PLN")),
            (Guid.NewGuid(), "Item 2", 1, Money.Create(10m, "USD"))
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Order.Create(items));
    }

    [Test]
    public void Create_WithValidItems_CreatesOrder()
    {
        // Arrange
        var items = new List<(Guid, string, int, Money)>
        {
            (Guid.NewGuid(), "Item 1", 1, _money),
            (Guid.NewGuid(), "Item 2", 2, _money)
        };

        // Act
        var order = Order.Create(items);

        // Assert
        Assert.That(order, Is.Not.Null);
        Assert.That(order.Items.Count, Is.EqualTo(2));
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Draft));
    }

    [Test]
    public void Submit_FromDraft_ChangesStatusToSubmitted()
    {
        // Arrange
        var order = Order.Create(new[]
        {
            (Guid.NewGuid(), "Item", 1, _money)
        });

        // Act
        order.Submit();

        // Assert
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Submitted));
    }

    [Test]
    public void Submit_FromSubmitted_Throws()
    {
        // Arrange
        var order = Order.Create(new[]
        {
            (Guid.NewGuid(), "Item", 1, _money)
        });
        order.Submit();

        // Act & Assert
        var ex = Assert.Throws<InvalidOrderStatusException>(() => order.Submit());
        Assert.That(ex!.Message, Does.Contain("Cannot submit order in status Submitted"));
    }

    [Test]
    public void AddItem_WithSameCurrency_AddsOrIncrements()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var order = Order.Create(new[]
        {
            (productId, "Item", 1, _money)
        });

        // Act
        order.AddItem(productId, "Item", 2, _money);

        // Assert
        var item = order.Items.First(i => i.ProductId == productId);
        Assert.That(item.Quantity, Is.EqualTo(3));
    }

    [Test]
    public void AddItem_WithDifferentCurrency_Throws()
    {
        // Arrange
        var order = Order.Create(new[]
        {
            (Guid.NewGuid(), "Item", 1, _money)
        });

        var usd = Money.Create(5m, "USD");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            order.AddItem(Guid.NewGuid(), "Another", 1, usd));
    }

    [Test]
    public void AddItem_WhenNotDraft_Throws()
    {
        // Arrange
        var order = Order.Create(new[]
        {
            (Guid.NewGuid(), "Item", 1, _money)
        });
        order.Submit();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            order.AddItem(Guid.NewGuid(), "Item 2", 1, _money));
    }

    [Test]
    public void AddItem_WhenOrderIsSubmitted_Throws()
    {
        // Arrange
        var order = Order.Create(new[]
        {
            (Guid.NewGuid(), "Item", 1, _money)
        });

        order.Submit(); // zmiana statusu na Submitted

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            order.AddItem(Guid.NewGuid(), "Another item", 1, _money));

        Assert.That(ex!.Message, Is.EqualTo("Only draft orders can be modified."));
    }
}
