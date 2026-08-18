namespace Application.Features.TenantFeatures.StaffBranches.Commands.CreateStaffBranch;

public sealed record CreateStaffBranchResponse(
    int StaffBranchId,
    int StaffId,
    int BranchId
);