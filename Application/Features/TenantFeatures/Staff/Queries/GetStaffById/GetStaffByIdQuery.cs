using Application.Abstractions.Queries;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Staff.Queries.GetStaffById;

public sealed record GetStaffByIdQuery(
    int StaffId
) : IQuery<Domain.Tenant.ServiceCloudTenant.Entities.Staff>;