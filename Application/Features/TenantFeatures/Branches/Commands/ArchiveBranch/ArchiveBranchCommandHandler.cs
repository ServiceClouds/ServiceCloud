using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Branches.Commands.ArchiveBranch;

public sealed class ArchiveBranchCommandHandler
    : ICommandHandler<ArchiveBranchCommand>
{
    private readonly ITenantRepository<Branch> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveBranchCommandHandler(
        ITenantRepository<Branch> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Find active Branch
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
        // 2. Archive through Domain Entity
        // ------------------------------------------------------------

        branch.Archive(_userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Update
        // ------------------------------------------------------------

        _repository.Update(branch);

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