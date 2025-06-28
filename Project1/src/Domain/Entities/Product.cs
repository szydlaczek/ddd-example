using Project1.Domain.ValueObjects;

namespace Project1.Domain.Entities;

public class Product : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;
    public Money Price { get; private set; } = null!;

    private Product()
    { } // EF Core

    private Product(string name, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        if (name.Length > 200)
            throw new ArgumentException("Product name cannot exceed 200 characters.");

        Name = name;

        Price = price;
        Id = SequentialGuid.SequentialGuidGenerator.Instance.NewGuid();
        //  AddDomainEvent(new ProductCreatedEvent(this));
    }

    public static Product Create(string name, Money price) => new Product(name, price);
}
