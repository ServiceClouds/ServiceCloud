using Application.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranches
{
    public sealed record GetServiceCategoryBranchesQuery
        : IQuery<GetServiceCategoryBranchesResponse>;
}
