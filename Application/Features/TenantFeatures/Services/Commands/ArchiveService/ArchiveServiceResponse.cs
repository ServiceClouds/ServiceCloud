namespace Application.Features.TenantFeatures.Services.Commands.ArchiveService;

public sealed record ArchiveServiceResponse(
    int ServiceId,
    string ServiceName
);