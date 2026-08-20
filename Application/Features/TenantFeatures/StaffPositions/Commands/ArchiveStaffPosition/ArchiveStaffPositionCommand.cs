using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.ArchiveStaffPosition;

public sealed record ArchiveStaffPositionCommand(
    int StaffPositionId
) : ICommand;