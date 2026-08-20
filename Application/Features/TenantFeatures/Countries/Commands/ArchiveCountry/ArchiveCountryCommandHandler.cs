using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Countries.Commands.ArchiveCountry;

public sealed class ArchiveCountryCommandHandler
    : ICommandHandler<ArchiveCountryCommand>
{
    private readonly ITenantRepository<Country> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveCountryCommandHandler(
        ITenantRepository<Country> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveCountryCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Find active Country
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
        // 2. Archive through Domain Entity
        // ------------------------------------------------------------

        country.Archive(_userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Update Repository
        // ------------------------------------------------------------

        _repository.Update(country);

        // ------------------------------------------------------------
        // 4. Save
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