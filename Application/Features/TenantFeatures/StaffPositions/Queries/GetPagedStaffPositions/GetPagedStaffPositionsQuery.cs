using Application.Abstractions.Queries;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StaffPositions.Queries.GetPagedStaffPositions;

public sealed record GetPagedStaffPositionsQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<StaffPosition>>;