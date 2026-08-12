using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Services.Commands.ArchiveService;

public sealed record ArchiveServiceCommand(
 
    int ServiceId
  
) : ICommand<ArchiveServiceResponse>;