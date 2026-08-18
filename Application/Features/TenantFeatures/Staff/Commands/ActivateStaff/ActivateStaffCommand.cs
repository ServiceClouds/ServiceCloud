using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Staff.Commands.ActivateStaff;

public sealed record ActivateStaffCommand(
    int StaffId
) : ICommand;