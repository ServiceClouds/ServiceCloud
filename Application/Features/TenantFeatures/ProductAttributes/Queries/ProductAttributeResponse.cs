namespace Application.Features.TenantFeatures.ProductAttributes.Queries;

public sealed record ProductAttributeResponse(
    int ProductAttributeId,
    int ProductId,
    int EAttributeId,
    int SortOrder
);
