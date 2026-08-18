using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch
{
    public sealed record CreateServiceCategoryBranchResponse(
     int ServiceCategoryBranchId,
     int ServiceCategoryId,
     int BranchId,
     bool IsActive,
     bool IsIncluded);
}
