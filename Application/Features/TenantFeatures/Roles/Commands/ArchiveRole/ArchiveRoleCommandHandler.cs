using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Roles.Commands.ArchiveRole;

public sealed class ArchiveRoleCommandHandler
    : ICommandHandler<ArchiveRoleCommand>
{
    private readonly ITenantRepository<Role> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveRoleCommandHandler(
        ITenantRepository<Role> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.RoleId == request.RoleId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (role is null)
        {
            return Result.Failure(
                Error.NotFound("Role not found."));
        }

        role.Archive(_userContext.StaffId);

        _repository.Update(role);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}