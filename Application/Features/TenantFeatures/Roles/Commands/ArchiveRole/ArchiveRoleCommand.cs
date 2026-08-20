using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Roles.Commands.ArchiveRole;

public sealed record ArchiveRoleCommand(
    int RoleId
) : ICommand;