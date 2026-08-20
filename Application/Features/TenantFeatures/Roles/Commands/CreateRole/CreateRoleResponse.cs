namespace Application.Features.TenantFeatures.Roles.Commands.CreateRole;

public sealed record CreateRoleResponse(
    int RoleId,
    string RoleName);