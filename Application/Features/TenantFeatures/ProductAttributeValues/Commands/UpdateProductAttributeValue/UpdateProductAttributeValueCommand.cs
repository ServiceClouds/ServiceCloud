using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.UpdateProductAttributeValue;

public sealed record UpdateProductAttributeValueCommand(
    long ProductAttributeValueId,
    int AttributeValueId
) : ICommand<UpdateProductAttributeValueResponse>;
