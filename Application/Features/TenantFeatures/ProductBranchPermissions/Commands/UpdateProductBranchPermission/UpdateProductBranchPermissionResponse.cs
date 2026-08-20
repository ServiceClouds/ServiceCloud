using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.UpdateProductBranchPermission
{
    public sealed record UpdateProductBranchPermissionResponse(
    
        int ProductBranchPermissionId,
          int ProductId,
        int BranchId);


    
}
