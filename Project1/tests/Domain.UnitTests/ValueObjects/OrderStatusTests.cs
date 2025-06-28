using NUnit.Framework;
using Project1.Domain.ValueObjects;

[TestFixture]
public class OrderStatusTests
{
    [Test]
    public void From_ValidValue_ReturnsExpectedInstance()
    {
        // Arrange
        var input = "Draft";

        // Act
        var result = OrderStatus.From(input);

        // Assert
        Assert.That(result, Is.EqualTo(OrderStatus.Draft));
    }

    [TestCase("draft")]
    [TestCase("DRAFT")]
    public void From_IsCaseInsensitive(string input)
    {
        // Arrange

        // Act
        var result = OrderStatus.From(input);

        // Assert
        Assert.That(result, Is.EqualTo(OrderStatus.Draft));
    }

    [Test]
    public void From_InvalidValue_Throws()
    {
        // Arrange
        var input = "Shipped";

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => OrderStatus.From(input));
        Assert.That(ex!.Message, Does.Contain("Unknown OrderStatus"));
    }

    [Test]
    public void PredefinedStatuses_AreNotNull()
    {
        // Arrange, Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(OrderStatus.Draft, Is.Not.Null);
            Assert.That(OrderStatus.Submitted, Is.Not.Null);
            Assert.That(OrderStatus.Paid, Is.Not.Null);
            Assert.That(OrderStatus.Cancelled, Is.Not.Null);
        });
    }

    [Test]
    public void Equality_WorksCorrectly()
    {
        // Arrange
        var a = OrderStatus.From("Submitted");
        var b = OrderStatus.Submitted;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(a == b, Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a != OrderStatus.Draft, Is.True);
        });
    }

    [Test]
    public void GetHashCode_IsConsistentForEqualObjects()
    {
        // Arrange
        var a = OrderStatus.From("Cancelled");
        var b = OrderStatus.From("CANCELLED");

        // Act
        var hashA = a.GetHashCode();
        var hashB = b.GetHashCode();

        // Assert
        Assert.That(hashA, Is.EqualTo(hashB));
    }

    [Test]
    public void ToString_ReturnsCorrectValue()
    {
        // Arrange
        var status = OrderStatus.Submitted;

        // Act
        var result = status.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("Submitted"));
    }
}
