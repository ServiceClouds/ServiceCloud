namespace Application.Features.TenantFeatures.Services.Commands.DeleteService;

public sealed record ArchiveServiceResponse(
    int ServiceId,
    string ServiceName
);