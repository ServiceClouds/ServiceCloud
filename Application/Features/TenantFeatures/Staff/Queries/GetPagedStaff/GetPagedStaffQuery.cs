using Application.Abstractions.Queries;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Staff.Queries.GetPagedStaff;

public sealed record GetPagedStaffQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Domain.Tenant.ServiceCloudTenant.Entities.Staff>>;