using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StaffBranches.Commands.ArchiveStaffBranch;

public sealed record ArchiveStaffBranchCommand(
    int StaffBranchId
) : ICommand;