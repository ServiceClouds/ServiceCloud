using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandHandler
    : ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly ITenantRepository<Role> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateRoleCommandHandler(
        ITenantRepository<Role> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateRoleResponse>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
        {
            return Result<CreateRoleResponse>.Failure(
                Error.Conflict("Role name is required."));
        }

        var role = Role.Create(
            request.RoleName.Trim(),
            _userContext.StaffId);

        _repository.Add(role);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateRoleResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateRoleResponse>.Success(
            new CreateRoleResponse(
                role.RoleId,
                role.RoleName));
    }
}