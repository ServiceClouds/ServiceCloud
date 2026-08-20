using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetProductAttributeById;

public sealed record GetProductAttributeByIdQuery(
    int ProductAttributeId
) : IQuery<ProductAttributeResponse>;
