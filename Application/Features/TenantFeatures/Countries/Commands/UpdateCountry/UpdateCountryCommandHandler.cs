using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Countries.Commands.UpdateCountry;

public sealed class UpdateCountryCommandHandler
    : ICommandHandler<UpdateCountryCommand>
{
    private readonly ITenantRepository<Country> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateCountryCommandHandler(
        ITenantRepository<Country> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateCountryCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Validate input
        // ------------------------------------------------------------

        if (string.IsNullOrWhiteSpace(request.CountryName))
        {
            return Result.Failure(
                Error.Conflict("Country name is required."));
        }

        // ------------------------------------------------------------
        // 2. Get active Country
        // ------------------------------------------------------------

        var country =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CountryId == request.CountryId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (country is null)
        {
            return Result.Failure(
                Error.NotFound("Country not found."));
        }

        // ------------------------------------------------------------
        // 3. Update through Domain Entity
        // ------------------------------------------------------------

        country.Update(
            request.CountryName.Trim(),
            string.IsNullOrWhiteSpace(request.CountryCode)
                ? null
                : request.CountryCode.Trim(),
            _userContext.StaffId);

        // ------------------------------------------------------------
        // 4. Update Repository
        // ------------------------------------------------------------

        _repository.Update(country);

        // ------------------------------------------------------------
        // 5. Save
        // ------------------------------------------------------------

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