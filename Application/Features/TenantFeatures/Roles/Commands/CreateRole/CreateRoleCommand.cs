using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(
    string RoleName
) : ICommand<CreateRoleResponse>;