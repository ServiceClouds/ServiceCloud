using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Staff.Commands.UpdateStaff;

public sealed class UpdateStaffCommandHandler
    : ICommandHandler<UpdateStaffCommand>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> _repository;
    private readonly ITenantRepository<StaffPosition> _staffPositionRepository;
    private readonly ITenantRepository<Country> _countryRepository;
    private readonly ITenantRepository<StateCountry> _stateCountryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateStaffCommandHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> repository,
        ITenantRepository<StaffPosition> staffPositionRepository,
        ITenantRepository<Country> countryRepository,
        ITenantRepository<StateCountry> stateCountryRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _staffPositionRepository = staffPositionRepository;
        _countryRepository = countryRepository;
        _stateCountryRepository = stateCountryRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateStaffCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Find Staff
        // ============================================================

        var staff =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffId == request.StaffId,
                    //&& !x.IsArchived,
                asNoTracking: false,
                cancellationToken);

        if (staff is null)
        {
            return Result.Failure(
                Error.NotFound("Staff not found."));
        }

        // ============================================================
        // 2. Validate Staff Position
        // ============================================================

        var staffPositionExists =
            await _staffPositionRepository.ExistsAsync(
                x =>
                    x.StaffPositionId == request.StaffPositionId &&
                    x.IsActive,
                cancellationToken);

        if (!staffPositionExists)
        {
            return Result.Failure(
                Error.NotFound("Staff position not found."));
        }

        // ============================================================
        // 3. Validate Country
        // ============================================================

        if (request.CountryId.HasValue)
        {
            var countryExists =
                await _countryRepository.ExistsAsync(
                    x =>
                        x.CountryId == request.CountryId.Value &&
                        x.IsActive,
                    cancellationToken);

            if (!countryExists)
            {
                return Result.Failure(
                    Error.NotFound("Country not found."));
            }
        }

        // ============================================================
        // 4. Validate StateCountry
        // ============================================================

        if (request.StateCountryId.HasValue)
        {
            var stateCountryExists =
                await _stateCountryRepository.ExistsAsync(
                    x =>
                        x.StateCountryId ==
                        request.StateCountryId.Value &&
                        x.IsActive,
                    cancellationToken);

            if (!stateCountryExists)
            {
                return Result.Failure(
                    Error.NotFound("State/Country not found."));
            }
        }

        // ============================================================
        // 5. Check Duplicate Email
        // ============================================================

        var duplicateEmail =
            await _repository.ExistsAsync(
                x =>
                    x.StaffId != request.StaffId &&
                    x.Email == request.Email ,
                    //&& !x.IsArchived,
                cancellationToken);

        if (duplicateEmail)
        {
            return Result.Failure(
                Error.Conflict("Staff email already exists."));
        }

        // ============================================================
        // 6. Update Domain Entity
        // ============================================================

        staff.Update(
            request.StaffPositionId,
            request.CountryId,
            request.StateCountryId,
            request.EnterpriseRoleId,
            request.EmploymentTypeId,
            request.ProbationDurationTypeId,
            request.Title,
            request.FirstName,
            request.LastName,
            //request.FullName,
            request.CardNumber,
            request.Email,
            request.Gender,
            request.BirthDate,
            request.Phone,
            request.Mobile,
            request.AddressLine1,
            request.AddressLine2,
            request.CityName,
            request.StateCountryName,
            request.PostCode,
            request.ImagePath,
            request.JoiningDate,
            request.ProbationMonths,
            request.ProbationValue,
            request.OrganizationalDate,
            request.EmploymentType,
            request.Notes,
            request.AllowLogin,
            request.IsSuperAdmin,
            _userContext.StaffId);

        // ============================================================
        // 7. Update Repository
        // ============================================================

        _repository.Update(staff);

        // ============================================================
        // 8. Save
        // ============================================================

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