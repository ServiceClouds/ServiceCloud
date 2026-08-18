using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.UpdateServiceCategoryBranch
{
    
    public sealed record UpdateServiceCategoryBranchCommand(
        int ServiceCategoryBranchId,
        bool IsActive,
        bool IsIncluded
    ) : ICommand<UpdateServiceCategoryBranchResponse>;
}

