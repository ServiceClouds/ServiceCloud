namespace Application.Features.TenantFeatures.Services.Commands.CreateService;

public sealed record CreateServiceResponse(
    int ServiceId,
    string ServiceName
);