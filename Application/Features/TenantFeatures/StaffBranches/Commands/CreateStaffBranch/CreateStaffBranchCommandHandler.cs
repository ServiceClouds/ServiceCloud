using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffBranches.Commands.CreateStaffBranch;

public sealed class CreateStaffBranchCommandHandler
    : ICommandHandler<CreateStaffBranchCommand, CreateStaffBranchResponse>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.StaffBranch>
        _repository;

    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff>
        _staffRepository;

    private readonly ITenantRepository<Branch>
        _branchRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateStaffBranchCommandHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.StaffBranch> repository,
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> staffRepository,
        ITenantRepository<Branch> branchRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _staffRepository = staffRepository;
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateStaffBranchResponse>> Handle(
        CreateStaffBranchCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Validate Staff
        // ============================================================

        var staffExists =
            await _staffRepository.ExistsAsync(
                x =>
                    x.StaffId == request.StaffId, 
                    //&&
                    //x.IsActive &&
                    //!x.IsArchived,
                cancellationToken);

        if (!staffExists)
        {
            return Result<CreateStaffBranchResponse>.Failure(
                Error.NotFound("Staff not found."));
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
            return Result<CreateStaffBranchResponse>.Failure(
                Error.NotFound("Branch not found."));
        }

        // ============================================================
        // 3. Create Domain Entity
        // ============================================================

        var staffBranch =
            Domain.Tenant.ServiceCloudTenant.Entities.StaffBranch.Create(
                request.StaffId,
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
        // 4. Add
        // ============================================================

        _repository.Add(staffBranch);

        // ============================================================
        // 5. Save
        // ============================================================

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateStaffBranchResponse>
                .Failure(saveResult.Error);
        }

        // ============================================================
        // 6. Response
        // ============================================================

        return Result<CreateStaffBranchResponse>.Success(
            new CreateStaffBranchResponse(
                staffBranch.StaffBranchId,
                staffBranch.StaffId,
                staffBranch.BranchId));
    }
}