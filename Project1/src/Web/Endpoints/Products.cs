using Microsoft.AspNetCore.Mvc;
using Project1.Application.Products.Commands.AddProduct;
using Project1.Application.Products.Queries.GetProducts;

namespace Project1.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup($"api/{nameof(Products)}");

        group.MapPost("/", AddProduct)
            .WithTags($"{nameof(Products)}");

        group.MapGet("/", GetProducts)
            .WithTags($"{nameof(Products)}");
    }

    public async Task<IResult> AddProduct(ISender sender, [FromBody] AddProductCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);

        return Results.Created($"/products/{result}", new { Id = result });
    }

    public async Task<IResult> GetProducts(ISender sender, [AsParameters] GetProductsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);

        return Results.Ok(result);
    }
}
