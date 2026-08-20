using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Roles.Queries.GetRoleById;

public sealed class GetRoleByIdQueryHandler
    : IQueryHandler<GetRoleByIdQuery, Role>
{
    private readonly ITenantRepository<Role> _repository;

    public GetRoleByIdQueryHandler(
        ITenantRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Role>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var role =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.RoleId == request.RoleId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (role is null)
        {
            return Result<Role>.Failure(
                Error.NotFound("Role not found."));
        }

        return Result<Role>.Success(role);
    }
}