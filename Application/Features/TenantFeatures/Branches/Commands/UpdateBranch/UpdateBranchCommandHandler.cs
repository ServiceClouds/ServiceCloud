using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Branches.Commands.UpdateBranch;

public sealed class UpdateBranchCommandHandler
    : ICommandHandler<UpdateBranchCommand>
{
    private readonly ITenantRepository<Branch> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateBranchCommandHandler(
        ITenantRepository<Branch> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Get Branch
        // ------------------------------------------------------------

        var branch =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.BranchId == request.BranchId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (branch is null)
        {
            return Result.Failure(
                Error.NotFound("Branch not found."));
        }

        // ------------------------------------------------------------
        // 2. Check duplicate Branch Code
        // ------------------------------------------------------------

        var duplicateCode =
            await _repository.ExistsAsync(
                x =>
                    x.BranchId != request.BranchId &&
                    x.BranchCode == request.BranchCode &&
                    x.IsActive,
                cancellationToken);

        if (duplicateCode)
        {
            return Result.Failure(
                Error.Conflict("Branch code already exists."));
        }

        // ------------------------------------------------------------
        // 3. Update Domain Entity
        // ------------------------------------------------------------

        branch.Update(
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
        // 4. Update Repository
        // ------------------------------------------------------------

        _repository.Update(branch);

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