using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Branches.Queries.GetBranchById;

public sealed record GetBranchByIdQuery(
    int BranchId
) : IQuery<Branch>;