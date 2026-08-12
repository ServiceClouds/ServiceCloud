namespace Application.Features.TenantFeatures.Services.Queries.GetPagedServices;

public sealed record GetPagedServicesResponse(
    int ServiceId,
    string? ServiceName,
    string? Description
);