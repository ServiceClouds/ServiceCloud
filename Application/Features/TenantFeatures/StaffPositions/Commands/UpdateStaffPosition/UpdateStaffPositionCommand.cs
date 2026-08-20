using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.UpdateStaffPosition;

public sealed record UpdateStaffPositionCommand(
    int StaffPositionId,
    string PositionName
) : ICommand;