using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Roles.Commands.UpdateRole;

public sealed record UpdateRoleCommand(
    int RoleId,
    string RoleName
) : ICommand;