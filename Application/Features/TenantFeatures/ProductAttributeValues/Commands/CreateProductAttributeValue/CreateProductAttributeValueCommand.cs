using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.CreateProductAttributeValue;

public sealed record CreateProductAttributeValueCommand(
    int productAttributeValueId,
    int ProductAttributeId,
    int AttributeValueId
) : ICommand<CreateProductAttributeValueResponse>;
