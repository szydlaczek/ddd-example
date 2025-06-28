using Project1.Application.Common.Interfaces;
using Project1.Application.Common.Models;

namespace Project1.Application.Products.Queries.GetProducts;

internal class GetProductsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetProductsQuery, PaginatedList<ProductsListItem>>
{
    public async Task<PaginatedList<ProductsListItem>> Handle(GetProductsQuery query, CancellationToken ct)
    {
        var mappedQuery = dbContext.Products.Select(ProductsListItem.Projection);

        return await PaginatedList<ProductsListItem>.CreateAsync(mappedQuery, query.PageNumber, query.PageSize, ct);
    }
}
