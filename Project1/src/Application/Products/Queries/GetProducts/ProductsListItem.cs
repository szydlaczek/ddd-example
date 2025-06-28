using System.Linq.Expressions;
using Project1.Domain.Entities;

namespace Project1.Application.Products.Queries.GetProducts;

public class ProductsListItem
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public decimal Price { get; private set; }

    public string Currency { get; private set; } = null!;

    public static Expression<Func<Product, ProductsListItem>> Projection
    {
        get => p => new ProductsListItem
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price.Amount,
            Currency = p.Price.Currency
        };
    }
}
