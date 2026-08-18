using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Companies.Commands.CreateCompany;

public sealed class CreateCompanyCommandHandler
    : ICommandHandler<CreateCompanyCommand, CreateCompanyResponse>
{
    private readonly ITenantRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateCompanyCommandHandler(
        ITenantRepository<Company> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateCompanyResponse>> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Check duplicate Company Code
        // ------------------------------------------------------------

        var companyCodeExists =
            await _repository.ExistsAsync(
                x =>
                    x.CompanyCode == request.CompanyCode &&
                    x.IsActive,
                cancellationToken);

        if (companyCodeExists)
        {
            return Result<CreateCompanyResponse>.Failure(
                Error.Conflict("Company code already exists."));
        }

        // ------------------------------------------------------------
        // 2. Create Domain Entity
        // ------------------------------------------------------------

        var company = Company.Create(
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
        // 3. Add Entity
        // ------------------------------------------------------------

        _repository.Add(company);

        // ------------------------------------------------------------
        // 4. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateCompanyResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 5. Response
        // ------------------------------------------------------------

        return Result<CreateCompanyResponse>.Success(
            new CreateCompanyResponse(
                company.CompanyId,
                company.CompanyCode));
    }
}