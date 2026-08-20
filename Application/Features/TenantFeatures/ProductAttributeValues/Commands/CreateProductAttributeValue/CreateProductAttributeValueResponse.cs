namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.CreateProductAttributeValue;

public sealed record CreateProductAttributeValueResponse(
    long ProductAttributeValueId,
    int ProductAttributeId,
    int AttributeValueId
);
