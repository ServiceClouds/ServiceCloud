using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.CreateProductBranchPermission
{
    public sealed record CreateProductBranchPermissionResponse(
    int ProductBranchPermissionId,
    int ProductId,
    int BranchId
);
}
