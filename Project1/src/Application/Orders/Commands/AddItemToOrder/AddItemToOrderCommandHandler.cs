using Project1.Application.Common.Exceptions;
using Project1.Application.Common.Interfaces;

namespace Project1.Application.Orders.Commands.AddItemToOrder;

internal class AddItemToOrderCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddItemToOrderCommand>
{
    public async Task Handle(AddItemToOrderCommand command, CancellationToken ct)
    {
        var order = await dbContext
            .Orders
            .FirstOrDefaultAsync(c => c.Id == command.OrderId, ct);

        AggregateNotFoundException.For(order, command.OrderId);

        var product = await dbContext
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.ProductId, ct);

        AggregateNotFoundException.For(product, command.ProductId);

        order?.AddItem(
            product!.Id,
            product.Name,
            command.Quantity,
            product.Price);

        await dbContext.SaveChangesAsync(ct);
    }
}
