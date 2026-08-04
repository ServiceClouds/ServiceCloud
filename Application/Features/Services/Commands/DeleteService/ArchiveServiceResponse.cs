namespace Application.Features.Services.Commands.ArchiveService;

public sealed record ArchiveServiceResponse(
    int ServiceId,
    string ServiceName
);