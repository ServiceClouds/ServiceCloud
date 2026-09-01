using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Staff.Commands.CreateStaff;

public sealed class CreateStaffCommandHandler
    : ICommandHandler<CreateStaffCommand, CreateStaffResponse>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> _repository;
    private readonly ITenantRepository<Company> _companyRepository;
    private readonly ITenantRepository<StaffPosition> _staffPositionRepository;
    private readonly ITenantRepository<Country> _countryRepository;
    private readonly ITenantRepository<StateCountry> _stateCountryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateStaffCommandHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> repository,
        ITenantRepository<Company> companyRepository,
        ITenantRepository<StaffPosition> staffPositionRepository,
        ITenantRepository<Country> countryRepository,
        ITenantRepository<StateCountry> stateCountryRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _companyRepository = companyRepository;
        _staffPositionRepository = staffPositionRepository;
        _countryRepository = countryRepository;
        _stateCountryRepository = stateCountryRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateStaffResponse>> Handle(
        CreateStaffCommand request,
        CancellationToken cancellationToken)
    {
        

        // ============================================================
        // 1. Validate Company
        // ============================================================

        var companyExists =
            await _companyRepository.ExistsAsync(
                x =>
                    x.CompanyId == request.CompanyId &&
                    x.IsActive,
                cancellationToken);

        if (!companyExists)
        {
            return Result<CreateStaffResponse>.Failure(
                Error.NotFound("Company not found."));
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
            return Result<CreateStaffResponse>.Failure(
                Error.NotFound("Staff position not found."));
        }

        // ============================================================
        // 3. Validate Country if supplied
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
                return Result<CreateStaffResponse>.Failure(
                    Error.NotFound("Country not found."));
            }
        }

        // ============================================================
        // 4. Validate StateCountry if supplied
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
                return Result<CreateStaffResponse>.Failure(
                    Error.NotFound("State/Country not found."));
            }
        }

        // ============================================================
        // 5. Check duplicate Email
        // ============================================================

        var emailExists =
            await _repository.ExistsAsync(
                x =>
                    x.Email == request.Email, 
                   // && !x.IsArchived,
                cancellationToken);

        if (emailExists)
        {
            return Result<CreateStaffResponse>.Failure(
                Error.Conflict("Staff email already exists."));
        }

        // ============================================================
        // 6. Create Domain Entity
        // ============================================================

        var staff = Domain.Tenant.ServiceCloudTenant.Entities.Staff.Create(
            request.CompanyId,
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
        // 7. Add
        // ============================================================

        _repository.Add(staff);

        // ============================================================
        // 8. Save
        // ============================================================

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateStaffResponse>
                .Failure(saveResult.Error);
        }

        // ============================================================
        // 9. Response
        // ============================================================

        return Result<CreateStaffResponse>.Success(
            new CreateStaffResponse(
                staff.StaffId,
                staff.Email));
    }
}