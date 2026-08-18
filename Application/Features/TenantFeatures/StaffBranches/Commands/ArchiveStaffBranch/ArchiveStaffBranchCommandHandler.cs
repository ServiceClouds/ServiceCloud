using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffBranches.Commands.ArchiveStaffBranch;

public sealed class ArchiveStaffBranchCommandHandler
    : ICommandHandler<ArchiveStaffBranchCommand>
{
    private readonly ITenantRepository<StaffBranch> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveStaffBranchCommandHandler(
        ITenantRepository<StaffBranch> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveStaffBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Find active StaffBranch
        // ============================================================

        var staffBranch =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffBranchId == request.StaffBranchId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (staffBranch is null)
        {
            return Result.Failure(
                Error.NotFound("Staff branch not found."));
        }

        // ============================================================
        // 2. Archive through Domain Entity
        // ============================================================

        staffBranch.Archive(
            _userContext.StaffId);

        // ============================================================
        // 3. Update
        // ============================================================

        _repository.Update(staffBranch);

        // ============================================================
        // 4. Save
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