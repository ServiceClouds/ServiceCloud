namespace Application.Features.TenantFeatures.StaffPositions.Commands.CreateStaffPosition;

public sealed record CreateStaffPositionResponse(
    int StaffPositionId,
    string PositionName);