namespace Application.Features.Services.Queries.GetServiceById;

public sealed record GetServiceByIdResponse(
    int ServiceId,
    int ServiceCategoryId,
    string? ServiceName,
    string? Description,
    string? SpecialInstruction,
    bool HasBranchPermission,
    bool AllowBranchEditPrice
);