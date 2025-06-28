namespace Project1.Application.Products.Commands.AddProduct;

public record AddProductCommand(string Name, decimal Price, string Currency)
    : IRequest<Guid>
{
}
