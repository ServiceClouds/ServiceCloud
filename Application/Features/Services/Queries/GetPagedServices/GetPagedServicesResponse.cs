namespace Application.Features.Services.Queries.GetPagedServices;

public sealed record GetPagedServicesResponse(
    int ServiceId,
    string? ServiceName,
    string? Description
);