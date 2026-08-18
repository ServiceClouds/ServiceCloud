using Application.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranchById
{
   
    public sealed record GetServiceCategoryBranchByIdQuery(
        int ServiceCategoryBranchId
    ) : IQuery<GetServiceCategoryBranchByIdResponse>;
}

