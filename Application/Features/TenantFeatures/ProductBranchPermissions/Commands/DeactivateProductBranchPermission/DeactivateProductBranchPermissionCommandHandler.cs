using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.DeactivateProductBranchPermission;


public sealed class DeactivateProductBranchPermissionCommandHandler
    : ICommandHandler<DeactivateProductBranchPermissionCommand>
{
    private readonly ITenantRepository<ProductBranchPermission> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateProductBranchPermissionCommandHandler(
        ITenantRepository<ProductBranchPermission> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeactivateProductBranchPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var permission =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductBranchPermissionId ==
                    request.ProductBranchPermissionId,
                cancellationToken: cancellationToken);

        if (permission is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Product branch permission was not found."));
        }

        permission.Deactivate();

        _repository.Update(permission);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}