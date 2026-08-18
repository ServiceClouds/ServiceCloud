using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.UpdateServiceCategoryBranch
{
    public sealed record UpdateServiceCategoryBranchResponse(
        int ServiceCategoryBranchId,
        int ServiceCategoryId,
        int BranchId,
        bool IsActive,
        bool IsIncluded
    );
}
