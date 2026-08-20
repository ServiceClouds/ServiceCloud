using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Countries.Queries.GetCountryById;

public sealed record GetCountryByIdQuery(
    int CountryId
) : IQuery<Country>;