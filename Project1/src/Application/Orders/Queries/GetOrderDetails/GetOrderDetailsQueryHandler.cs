using Project1.Application.Common.Exceptions;
using Project1.Application.Common.Interfaces;
using Project1.Domain.Entities;

namespace Project1.Application.Orders.Queries.GetOrderDetails;

internal class GetOrderDetailsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<OrderDetailsDto> Handle(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var order = await dbContext
            .Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.Id, ct);

        AggregateNotFoundException.For(order, query.Id);

        return MapOrderToDto(order!);
    }

    public OrderDetailsDto MapOrderToDto(Order order)
    {
        return new OrderDetailsDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Status = order.Status.Value,
            TotalAmount = $"{order.TotalAmount.Amount:0.00} {order.TotalAmount.Currency}",
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = $"{i.UnitPrice.Amount:0.00} {i.UnitPrice.Currency}",
                Total = $"{i.Total.Amount:0.00} {i.Total.Currency}"
            }).ToList()
        };
    }
}
