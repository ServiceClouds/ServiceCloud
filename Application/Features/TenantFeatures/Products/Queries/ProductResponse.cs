namespace Application.Features.TenantFeatures.Products.Queries;

public sealed record ProductResponse(
    int ProductId,
    int ProductCategoryId,
    string? ProductName,
    string? Description,
    bool? IsActive,
    bool AllowBranchTrackInventory,
    bool HasBranchPermission,
    bool AllowBranchEditPrice,
    int? ProductClassificationId,
    int? BrandId,
    int? AppSourceTypeId
);
