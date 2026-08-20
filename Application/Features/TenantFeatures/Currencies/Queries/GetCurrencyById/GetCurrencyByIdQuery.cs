using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Application.Features.TenantFeatures.Currencies.Queries.GetCurrencyById;

public sealed record GetCurrencyByIdQuery(
    int CurrencyId
) : IQuery<Currency>;