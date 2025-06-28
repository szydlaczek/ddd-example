namespace Project1.Application.Orders.Commands.AddItemToOrder;

public record AddItemToOrderRequest(Guid ProductId, int Quantity);
