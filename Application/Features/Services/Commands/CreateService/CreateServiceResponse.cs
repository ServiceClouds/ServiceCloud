namespace Application.Features.Services.Commands.CreateService;

public sealed record CreateServiceResponse(
    int ServiceId,
    string ServiceName
);