namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.UpdateProductAttributeValue;

public sealed record UpdateProductAttributeValueResponse(
    long ProductAttributeValueId,
    int ProductAttributeId,
    int AttributeValueId
);
