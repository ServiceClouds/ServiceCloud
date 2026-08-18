using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Staff.Commands.ArchiveStaff;

public sealed record ArchiveStaffCommand(
    int StaffId
) : ICommand;