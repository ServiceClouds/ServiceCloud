using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Countries.Queries.GetCountryById;

public sealed class GetCountryByIdQueryHandler
    : IQueryHandler<GetCountryByIdQuery, Country>
{
    private readonly ITenantRepository<Country> _repository;

    public GetCountryByIdQueryHandler(
        ITenantRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Country>> Handle(
        GetCountryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var country =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CountryId == request.CountryId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (country is null)
        {
            return Result<Country>.Failure(
                Error.NotFound("Country not found."));
        }

        return Result<Country>.Success(country);
    }
}