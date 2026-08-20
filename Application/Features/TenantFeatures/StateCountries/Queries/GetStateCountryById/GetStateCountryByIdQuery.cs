using Application.Abstractions.Queries;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StateCountries.Queries.GetStateCountryById;

public sealed record GetStateCountryByIdQuery(
    int StateCountryId
) : IQuery<StateCountry>;