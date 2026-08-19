using Application.Abstractions.Queries;
using Application.Common;

namespace Application.Features.TenantFeatures.Products.Queries.GetPagedProducts;

public sealed record GetPagedProductsQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<ProductResponse>>;
