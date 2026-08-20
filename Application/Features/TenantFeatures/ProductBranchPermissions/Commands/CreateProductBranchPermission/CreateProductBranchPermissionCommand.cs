using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.CreateProductBranchPermission
{
    public sealed record CreateProductBranchPermissionCommand(
    int ProductId,
    int BranchId,
    bool IsActive,
    bool IsOnline,
    bool IsHidePriceOnline,
    bool IsFeatured,
    bool HasTrackingventory,
    bool HasShipping,
    bool IsIncluded,
    bool BusinessUseOnly,
    bool IsVariantGenerated,
    bool IsSharedPrivately
) : ICommand<CreateProductBranchPermissionResponse>;
}
