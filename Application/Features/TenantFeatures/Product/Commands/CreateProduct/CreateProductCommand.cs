using MediatR;

namespace Application.Features.TenantFeatures.Product.Commands.CreateProduct;

public sealed record CreateProductCommand(
    int ProductCategoryId,
    string? ProductName,
    string? Description,
    bool? IsActive = true,
    bool AllowBranchTrackInventory = false,
    bool HasBranchPermission = false,
    bool AllowBranchEditPrice = false,
    int? ProductClassificationId = null,
    int? BrandId = null,
    int? AppSourceTypeId = null
) : IRequest<CreateProductResponse>;
