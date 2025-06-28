namespace Project1.Application.Orders.Queries.GetOrderDetails;

public class OrderItemDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = null!;
    public int Quantity { get; init; }
    public string UnitPrice { get; init; } = null!; // np. "12.34 PLN"
    public string Total { get; init; } = null!;
}
