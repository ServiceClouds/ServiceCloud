using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetAllProductAttributes;

public sealed record GetAllProductAttributesQuery
    : IQuery<List<ProductAttributeResponse>>;
