using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Companies.Queries.GetPagedCompanies;

public sealed record GetPagedCompaniesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Company>>;