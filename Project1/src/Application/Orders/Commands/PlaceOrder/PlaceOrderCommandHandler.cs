using Project1.Application.Common.Exceptions;
using Project1.Application.Common.Interfaces;
using Project1.Domain.Entities;

namespace Project1.Application.Orders.Commands.PlaceOrder;

internal class PlaceOrderCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<PlaceOrderCommand, Guid>
{
    public async Task<Guid> Handle(PlaceOrderCommand command, CancellationToken ct)
    {
        var products = await dbContext
            .Products
            .Where(c => command.Items.Select(i => i.ProductId).Contains(c.Id))
            .ToListAsync(ct);

        var items = command.Items.Select(i =>
        {
            var product = products.SingleOrDefault(p => p.Id == i.ProductId);

            AggregateNotFoundException.For(product, i.ProductId);

            return (product!.Id, product.Name, i.Quantity, product.Price);
        }).ToList();

        var order = Order.Create(items);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(ct);

        return order.Id;
    }
}
