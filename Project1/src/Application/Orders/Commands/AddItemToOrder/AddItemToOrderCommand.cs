namespace Project1.Application.Orders.Commands.AddItemToOrder;
public record AddItemToOrderCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity) : IRequest;
