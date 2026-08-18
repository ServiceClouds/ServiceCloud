using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StaffBranches.Commands.UpdateStaffBranch;

public sealed record UpdateStaffBranchCommand(
    int StaffBranchId,
    int BranchId,
    int? RoleId,
    int? DialerStatusTypeId,
    string? OnlineDisplayName,
    bool ShowOnScheduler,
    bool CanDoClass,
    bool CanDoService,
    bool CanDoServiceOnline,
    bool CanDoCourse,
    bool FirstAidAllowed,
    bool AllowTip,
    bool DoorAccessAllowed
) : ICommand;