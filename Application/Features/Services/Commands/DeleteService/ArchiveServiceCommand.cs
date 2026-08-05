using Application.Abstractions.Commands;

namespace Application.Features.Services.Commands.ArchiveService;

public sealed record ArchiveServiceCommand(
 
    int ServiceId
  
) : ICommand<ArchiveServiceResponse>;