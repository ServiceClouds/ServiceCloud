using Application.Abstractions.Queries;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Roles.Queries.GetPagedRoles;

public sealed record GetPagedRolesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Role>>;