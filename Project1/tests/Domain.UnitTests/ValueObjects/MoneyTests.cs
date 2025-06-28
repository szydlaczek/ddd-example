using NUnit.Framework;
using Project1.Domain.ValueObjects;

[TestFixture]
public class MoneyTests
{
    [Test]
    public void Create_WithNegativeAmount_Throws()
    {
        // Arrange
        var amount = -10m;
        var currency = "PLN";

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => Money.Create(amount, currency));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Create_WithEmptyCurrency_Throws(string currency)
    {
        // Arrange
        var amount = 10m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Money.Create(amount, currency));
    }

    [Test]
    public void Create_WithInvalidCurrencyLength_Throws()
    {
        // Arrange
        var amount = 10m;
        var currency = "EURO"; // too long

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Money.Create(amount, currency));
    }

    [Test]
    public void Create_WithSameValues_IsEqual()
    {
        // Arrange
        var a = Money.Create(100m, "PLN");
        var b = Money.Create(100m, "pln");

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a == b, Is.True);
            Assert.That(a != Money.Create(200m, "PLN"), Is.True);
        });
    }

    [Test]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var money = Money.Create(12.5m, "eur");

        // Act
        var result = money.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("12,50 EUR")); // zależy od Twojej ToString()
    }

    [Test]
    public void GetHashCode_IsConsistentForEqualObjects()
    {
        // Arrange
        var a = Money.Create(42.00m, "usd");
        var b = Money.Create(42.00m, "USD");

        // Act
        var hashA = a.GetHashCode();
        var hashB = b.GetHashCode();

        // Assert
        Assert.That(hashA, Is.EqualTo(hashB));
    }
}
