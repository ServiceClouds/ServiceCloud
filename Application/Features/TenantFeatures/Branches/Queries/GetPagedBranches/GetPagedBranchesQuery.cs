using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Branches.Queries.GetPagedBranches;

public sealed record GetPagedBranchesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Branch>>;