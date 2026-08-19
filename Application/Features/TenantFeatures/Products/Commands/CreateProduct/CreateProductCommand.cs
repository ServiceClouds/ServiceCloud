using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
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
) : ICommand<CreateProductResponse>;
