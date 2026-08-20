using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.CreateStaffPosition;

public sealed record CreateStaffPositionCommand(
    string PositionName
) : ICommand<CreateStaffPositionResponse>;