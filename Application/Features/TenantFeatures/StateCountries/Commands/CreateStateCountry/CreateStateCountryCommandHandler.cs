using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StateCountries.Commands.CreateStateCountry;

public sealed class CreateStateCountryCommandHandler
    : ICommandHandler<CreateStateCountryCommand, CreateStateCountryResponse>
{
    private readonly ITenantRepository<StateCountry> _repository;
    private readonly ITenantRepository<Country> _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateStateCountryCommandHandler(
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

    public async Task<Result<CreateStateCountryResponse>> Handle(
        CreateStateCountryCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.StateCountryName))
        {
            return Result<CreateStateCountryResponse>.Failure(
                Error.Conflict("State/Country name is required."));
        }

        var country =
            await _countryRepository.FirstOrDefaultAsync(
                x =>
                    x.CountryId == request.CountryId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (country is null)
        {
            return Result<CreateStateCountryResponse>.Failure(
                Error.NotFound("Country not found."));
        }

        var stateCountry = StateCountry.Create(
            request.StateCountryName.Trim(),
            request.CountryId,
            _userContext.StaffId);

        _repository.Add(stateCountry);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateStateCountryResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateStateCountryResponse>.Success(
            new CreateStateCountryResponse(
                stateCountry.StateCountryId,
                stateCountry.StateCountryName,
                stateCountry.CountryId));
    }
}