namespace Application.Features.Services.Commands.UpdateService;

public sealed record UpdateServiceResponse(
    int ServiceId,
    string ServiceName
);