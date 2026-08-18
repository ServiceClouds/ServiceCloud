using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffBranches.Commands.UpdateStaffBranch;

public sealed class UpdateStaffBranchCommandHandler
    : ICommandHandler<UpdateStaffBranchCommand>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.StaffBranch>
        _repository;

    private readonly ITenantRepository<Branch>
        _branchRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateStaffBranchCommandHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.StaffBranch> repository,
        ITenantRepository<Branch> branchRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateStaffBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Get StaffBranch
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
        // 2. Validate Branch
        // ============================================================

        var branchExists =
            await _branchRepository.ExistsAsync(
                x =>
                    x.BranchId == request.BranchId &&
                    x.IsActive,
                cancellationToken);

        if (!branchExists)
        {
            return Result.Failure(
                Error.NotFound("Branch not found."));
        }

        // ============================================================
        // 3. Update Domain Entity
        // ============================================================

        staffBranch.Update(
            request.BranchId,
            request.RoleId,
            request.DialerStatusTypeId,
            request.OnlineDisplayName,
            request.ShowOnScheduler,
            request.CanDoClass,
            request.CanDoService,
            request.CanDoServiceOnline,
            request.CanDoCourse,
            request.FirstAidAllowed,
            request.AllowTip,
            request.DoorAccessAllowed,
            _userContext.StaffId);

        // ============================================================
        // 4. Update Repository
        // ============================================================

        _repository.Update(staffBranch);

        // ============================================================
        // 5. Save
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