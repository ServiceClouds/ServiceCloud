namespace Application.Features.TenantFeatures.Services.Commands.UpdateService;

public sealed record UpdateServiceResponse(
    int ServiceId,
    string ServiceName
);