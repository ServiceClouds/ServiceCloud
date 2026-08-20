using Application.Abstractions.Queries;
using Application.Common;
using Application.Features.TenantFeatures.ProductAttributes.Queries;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetPagedProductAttributes;

public sealed record GetPagedProductAttributesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<ProductAttributeResponse>>;
