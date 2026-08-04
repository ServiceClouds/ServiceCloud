using Application.Abstractions.Commands;

namespace Application.Features.Services.Commands.ArchiveService;

public sealed record ArchiveServiceCommand(
    int CompanyId,
    int ServiceId,
    int ModifiedBy
) : ICommand<ArchiveServiceResponse>;