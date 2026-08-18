using Application.Abstractions.Queries;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StaffBranches.Queries.GetStaffBranchById;

public sealed record GetStaffBranchByIdQuery(
    int StaffBranchId
) : IQuery<StaffBranch>;