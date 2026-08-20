namespace Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;

public sealed record CreateProductAttributeResponse(
    int ProductAttributeId,
    int ProductId,
    int EAttributeId,
    int SortOrder
);
