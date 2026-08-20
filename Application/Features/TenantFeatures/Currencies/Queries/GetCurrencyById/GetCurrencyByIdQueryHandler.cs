using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Currencies.Queries.GetCurrencyById;

public sealed class GetCurrencyByIdQueryHandler
    : IQueryHandler<GetCurrencyByIdQuery, Currency>
{
    private readonly ITenantRepository<Currency> _repository;

    public GetCurrencyByIdQueryHandler(
        ITenantRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Currency>> Handle(
        GetCurrencyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var currency =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CurrencyId == request.CurrencyId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (currency is null)
        {
            return Result<Currency>.Failure(
                Error.NotFound("Currency not found."));
        }

        return Result<Currency>.Success(currency);
    }
}