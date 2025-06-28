namespace Project1.Application.Orders.Commands.PlaceOrder;
public record class PlaceOrderCommand(List<PlaceOrderItemDto> Items) : IRequest<Guid>;
