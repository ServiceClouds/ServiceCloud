using Application.Abstractions.Queries;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StaffPositions.Queries.GetStaffPositionById;

public sealed record GetStaffPositionByIdQuery(
    int StaffPositionId
) : IQuery<StaffPosition>;