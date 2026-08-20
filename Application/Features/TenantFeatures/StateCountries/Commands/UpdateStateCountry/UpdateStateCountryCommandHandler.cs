using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StateCountries.Commands.UpdateStateCountry;

public sealed class UpdateStateCountryCommandHandler
    : ICommandHandler<UpdateStateCountryCommand>
{
    private readonly ITenantRepository<StateCountry> _repository;
    private readonly ITenantRepository<Country> _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateStateCountryCommandHandler(
        ITenantRepository<StateCountry> repository,
        ITenantRepository<Country> countryRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateStateCountryCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.StateCountryName))
        {
            return Result.Failure(
                Error.Conflict("State/Country name is required."));
        }

        var stateCountry =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StateCountryId == request.StateCountryId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (stateCountry is null)
        {
            return Result.Failure(
                Error.NotFound("State/Country not found."));
        }

        var country =
            await _countryRepository.FirstOrDefaultAsync(
                x =>
                    x.CountryId == request.CountryId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (country is null)
        {
            return Result.Failure(
                Error.NotFound("Country not found."));
        }

        stateCountry.Update(
            request.StateCountryName.Trim(),
            request.CountryId,
            _userContext.StaffId);

        _repository.Update(stateCountry);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}