namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries;

public sealed record ProductAttributeValueResponse(
    long ProductAttributeValueId,
    int ProductAttributeId,
    int AttributeValueId
);
