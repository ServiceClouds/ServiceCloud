using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Countries.Commands.CreateCountry;

public sealed class CreateCountryCommandHandler
    : ICommandHandler<CreateCountryCommand, CreateCountryResponse>
{
    private readonly ITenantRepository<Country> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateCountryCommandHandler(
        ITenantRepository<Country> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateCountryResponse>> Handle(
        CreateCountryCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Validate input
        // ------------------------------------------------------------

        if (string.IsNullOrWhiteSpace(request.CountryName))
        {
            return Result<CreateCountryResponse>.Failure(
                Error.Conflict("Country name is required."));
        }

        // ------------------------------------------------------------
        // 2. Create Domain Entity
        // ------------------------------------------------------------

        var country = Country.Create(
            request.CountryName.Trim(),
            string.IsNullOrWhiteSpace(request.CountryCode)
                ? null
                : request.CountryCode.Trim(),
            _userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Add Entity
        // ------------------------------------------------------------

        _repository.Add(country);

        // ------------------------------------------------------------
        // 4. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateCountryResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 5. Response
        // ------------------------------------------------------------

        return Result<CreateCountryResponse>.Success(
            new CreateCountryResponse(
                country.CountryId,
                country.CountryName));
    }
}