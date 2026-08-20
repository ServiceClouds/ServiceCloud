using Application.Abstractions.Queries;
using Application.Common;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetPagedProductAttributeValues;

public sealed record GetPagedProductAttributeValuesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<ProductAttributeValueResponse>>;
