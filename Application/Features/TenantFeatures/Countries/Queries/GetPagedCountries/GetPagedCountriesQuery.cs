using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Countries.Queries.GetPagedCountries;

public sealed record GetPagedCountriesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Country>>;