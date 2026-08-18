using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.DeleteServiceCategoryBranch
{
    public sealed record DeleteServiceCategoryBranchCommand(
       int ServiceCategoryBranchId
   ) : ICommand;
}
