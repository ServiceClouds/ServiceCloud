using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Companies.Queries.GetCompanyById;

public sealed record GetCompanyByIdQuery(
    int CompanyId
) : IQuery<Company>;