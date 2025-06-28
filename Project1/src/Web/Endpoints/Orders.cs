using Microsoft.AspNetCore.Mvc;
using Project1.Application.Orders.Commands.AddItemToOrder;
using Project1.Application.Orders.Commands.PlaceOrder;
using Project1.Application.Orders.Commands.SubmitOrder;
using Project1.Application.Orders.Queries.GetOrderDetails;

namespace Project1.Web.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup($"api/{nameof(Orders)}");

        group.MapPost("/", PlaceOrder)
            .WithTags($"{nameof(Orders)}");

        group.MapGet("/{id:guid}", GetOrderDetails)
            .WithTags($"{nameof(Orders)}");

        group.MapPost("/{id:guid}/submit", SubmitOrder)
            .WithTags($"{nameof(Orders)}");

        group.MapPost("/{id:guid}/items", AddItem)
            .WithTags($"{nameof(Orders)}");
    }

    public async Task<IResult> PlaceOrder(ISender sender, [FromBody] PlaceOrderCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);

        return Results.Created($"/Orders/{result}", new { Id = result });
    }

    public async Task<IResult> GetOrderDetails(ISender sender, Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetOrderDetailsQuery(id), ct);

        return Results.Ok(result);
    }

    public async Task<IResult> SubmitOrder(ISender sender, Guid id, CancellationToken ct)
    {
        await sender.Send(new SubmitOrderCommand(id), ct);

        return Results.NoContent();
    }

    public async Task<IResult> AddItem(ISender sender, Guid id, [FromBody] AddItemToOrderRequest request, CancellationToken ct)
    {
        var command = new AddItemToOrderCommand(id, request.ProductId, request.Quantity);

        await sender.Send(command, ct);

        return Results.NoContent();
    }
}
