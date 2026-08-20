using Application.Abstractions.Queries;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Roles.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(
    int RoleId
) : IQuery<Role>;