using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
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
) : ICommand<UpdateProductResponse>;
