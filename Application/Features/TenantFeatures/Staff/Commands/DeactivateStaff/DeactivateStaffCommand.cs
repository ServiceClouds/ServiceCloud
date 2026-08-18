using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Staff.Commands.DeactivateStaff;

public sealed record DeactivateStaffCommand(
    int StaffId
) : ICommand;