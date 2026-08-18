using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch
{
    public sealed record CreateServiceCategoryBranchCommand(
    int ServiceCategoryId,
    int BranchId,
    bool IsIncluded = true
) : ICommand<CreateServiceCategoryBranchResponse>;
}
