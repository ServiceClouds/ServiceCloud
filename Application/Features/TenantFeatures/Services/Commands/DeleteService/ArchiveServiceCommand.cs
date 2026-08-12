using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Services.Commands.DeleteService;

public sealed record ArchiveServiceCommand(
 
    int ServiceId
  
) : ICommand<ArchiveServiceResponse>;