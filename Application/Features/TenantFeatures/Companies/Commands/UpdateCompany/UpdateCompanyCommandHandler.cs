using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Companies.Commands.UpdateCompany;

public sealed class UpdateCompanyCommandHandler
    : ICommandHandler<UpdateCompanyCommand>
{
    private readonly ITenantRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateCompanyCommandHandler(
        ITenantRepository<Company> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Get Company
        // ------------------------------------------------------------

        var company =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CompanyId == request.CompanyId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (company is null)
        {
            return Result.Failure(
                Error.NotFound("Company not found."));
        }

        // ------------------------------------------------------------
        // 2. Check duplicate Company Code
        // ------------------------------------------------------------

        var duplicateCode =
            await _repository.ExistsAsync(
                x =>
                    x.CompanyId != request.CompanyId &&
                    x.CompanyCode == request.CompanyCode &&
                    x.IsActive,
                cancellationToken);

        if (duplicateCode)
        {
            return Result.Failure(
                Error.Conflict("Company code already exists."));
        }

        // ------------------------------------------------------------
        // 3. Update Domain Entity
        // ------------------------------------------------------------

        company.Update(
            request.CountryId,
            request.CurrencyId,
            request.CompanyName,
            request.CompanyCode,
            request.Ntn,
            request.RegistrationNumber,
            request.Email,
            request.Website,
            request.Phone,
            request.Fax,
            request.AddressLine1,
            request.AddressLine2,
            request.CityName,
            request.StateCountryName,
            request.PostalCode,
            request.ImagePath,
            request.AppleStoreUrl,
            request.GooglePlayStoreUrl,
            _userContext.StaffId);

        // ------------------------------------------------------------
        // 4. Update Repository
        // ------------------------------------------------------------

        _repository.Update(company);

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