using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Application.Common.Models;

namespace Project1.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<PaginatedList<ProductsListItem>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
