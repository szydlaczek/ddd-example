using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Application.Orders.Queries.GetOrderDetails;

public class OrderDetailsDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Status { get; init; } = null!;
    public string TotalAmount { get; init; } = null!; // np. "123.45 PLN"

    public List<OrderItemDto> Items { get; init; } = new();
}
