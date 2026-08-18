using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranchById
{
    public sealed record GetServiceCategoryBranchByIdResponse(
        int ServiceCategoryBranchId,
        int ServiceCategoryId,
        int BranchId,
        bool IsActive,
        bool IsIncluded
    );
}
