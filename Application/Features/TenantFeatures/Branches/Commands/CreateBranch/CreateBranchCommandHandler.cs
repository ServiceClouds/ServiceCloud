using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Branches.Commands.CreateBranch;

public sealed class CreateBranchCommandHandler
    : ICommandHandler<CreateBranchCommand, CreateBranchResponse>
{
    private readonly ITenantRepository<Branch> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateBranchCommandHandler(
        ITenantRepository<Branch> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateBranchResponse>> Handle(
        CreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Check duplicate Branch Code
        // ------------------------------------------------------------

        var branchCodeExists =
            await _repository.ExistsAsync(
                x =>
                    x.BranchCode == request.BranchCode &&
                    x.IsActive,
                cancellationToken);

        if (branchCodeExists)
        {
            return Result<CreateBranchResponse>.Failure(
                Error.Conflict("Branch code already exists."));
        }

        // ------------------------------------------------------------
        // 2. Create Domain Entity
        // ------------------------------------------------------------

        var branch = Branch.Create(
            request.CompanyId,
            request.CountryId,
            request.BranchName,
            request.BranchCode,
            request.CityName,
            request.StateCountryName,
            request.AddressLine1,
            request.AddressLine2,
            request.PostalCode,
            request.Email,
            request.Phone,
            request.Mobile,
            request.Fax,
            request.TimeZone,
            request.Currency,
            request.DateFormatId,
            request.TermsOfServiceUrl,
            request.PrivacyPolicyUrl,
            request.IsOnline,
            _userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Add Entity
        // ------------------------------------------------------------

        _repository.Add(branch);

        // ------------------------------------------------------------
        // 4. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateBranchResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 5. Response
        // ------------------------------------------------------------

        return Result<CreateBranchResponse>.Success(
            new CreateBranchResponse(
                branch.BranchId,
                branch.BranchCode));
    }
}