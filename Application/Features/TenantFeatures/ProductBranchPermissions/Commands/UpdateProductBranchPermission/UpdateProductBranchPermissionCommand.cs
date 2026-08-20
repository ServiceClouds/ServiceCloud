using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.UpdateProductBranchPermission
{
    public sealed record UpdateProductBranchPermissionCommand(
    int ProductBranchPermissionId,
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
) : ICommand<UpdateProductBranchPermissionResponse>;
}
