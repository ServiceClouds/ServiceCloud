using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetProductAttributeValueById;

public sealed record GetProductAttributeValueByIdQuery(
    long ProductAttributeValueId
) : IQuery<ProductAttributeValueResponse>;
