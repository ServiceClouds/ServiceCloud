using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Branches.Commands.ArchiveBranch;

public sealed record ArchiveBranchCommand(
    int BranchId
) : ICommand;