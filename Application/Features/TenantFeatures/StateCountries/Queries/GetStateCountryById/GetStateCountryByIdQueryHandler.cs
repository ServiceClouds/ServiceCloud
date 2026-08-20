using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StateCountries.Queries.GetStateCountryById;

public sealed class GetStateCountryByIdQueryHandler
    : IQueryHandler<GetStateCountryByIdQuery, StateCountry>
{
    private readonly ITenantRepository<StateCountry> _repository;

    public GetStateCountryByIdQueryHandler(
        ITenantRepository<StateCountry> repository)
    {
        _repository = repository;
    }

    public async Task<Result<StateCountry>> Handle(
        GetStateCountryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var stateCountry =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StateCountryId == request.StateCountryId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (stateCountry is null)
        {
            return Result<StateCountry>.Failure(
                Error.NotFound("State/Country not found."));
        }

        return Result<StateCountry>.Success(
            stateCountry);
    }
}