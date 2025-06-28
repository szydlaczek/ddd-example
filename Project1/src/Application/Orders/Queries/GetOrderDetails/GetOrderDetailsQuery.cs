namespace Project1.Application.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(Guid Id) : IRequest<OrderDetailsDto>;
