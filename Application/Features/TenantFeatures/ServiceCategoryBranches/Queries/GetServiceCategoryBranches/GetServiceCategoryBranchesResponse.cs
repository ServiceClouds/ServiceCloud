using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranches
{
    public sealed record GetServiceCategoryBranchesResponse(
        IReadOnlyList<ServiceCategoryBranchItemResponse> Items
    );

    public sealed record ServiceCategoryBranchItemResponse(
        int ServiceCategoryBranchId,
        int ServiceCategoryId,
        int BranchId,
        bool IsActive,
        bool IsIncluded
    );
}
