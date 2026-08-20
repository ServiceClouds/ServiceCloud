using Application.Abstractions.Queries;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.StateCountries.Queries.GetPagedStateCountries;

public sealed record GetPagedStateCountriesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<StateCountry>>;