using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetAllProductAttributeValues;

public sealed record GetAllProductAttributeValuesQuery
    : IQuery<List<ProductAttributeValueResponse>>;
