using Application.Abstractions.Queries;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StaffBranches.Queries.GetPagedStaffBranches;

public sealed record GetPagedStaffBranchesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<StaffBranch>>;