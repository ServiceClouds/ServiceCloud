namespace Application.Features.TenantFeatures.ProductAttributes.Commands.UpdateProductAttribute;

public sealed record UpdateProductAttributeResponse(
    int ProductAttributeId,
    int ProductId,
    int EAttributeId,
    int SortOrder
);
