using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Currencies.Queries.GetPagedCurrencies;

public sealed record GetPagedCurrenciesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Currency>>;