using Project1.Application.Common.Interfaces;
using Project1.Domain.Entities;
using Project1.Domain.ValueObjects;

namespace Project1.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<AddProductCommand, Guid>
{
    public async Task<Guid> Handle(AddProductCommand command, CancellationToken ct)
    {
        var product = Product.Create(command.Name, Money.Create(command.Price, command.Currency));

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync(ct);

        return product.Id;
    }
}
