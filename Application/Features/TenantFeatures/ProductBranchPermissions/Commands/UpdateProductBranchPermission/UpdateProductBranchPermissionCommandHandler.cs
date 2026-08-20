using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.UpdateProductBranchPermission
{


    public sealed class UpdateProductBranchPermissionCommandHandler
        : ICommandHandler<
            UpdateProductBranchPermissionCommand,
            UpdateProductBranchPermissionResponse>
    {
        private readonly ITenantRepository<ProductBranchPermission> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductBranchPermissionCommandHandler(
            ITenantRepository<ProductBranchPermission> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdateProductBranchPermissionResponse>> Handle(
            UpdateProductBranchPermissionCommand request,
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
                return Result<UpdateProductBranchPermissionResponse>.Failure(
                    Error.NotFound(
                        "Product branch permission was not found."));
            }

            permission.Update(
                request.IsActive,
                request.IsOnline,
                request.IsHidePriceOnline,
                request.IsFeatured,
                request.HasTrackingventory,
                request.HasShipping,
                request.IsIncluded,
                request.BusinessUseOnly,
                request.IsVariantGenerated,
                request.IsSharedPrivately);

            _repository.Update(permission);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<UpdateProductBranchPermissionResponse>
                    .Failure(saveResult.Error);
            }

            return Result<UpdateProductBranchPermissionResponse>.Success(
                new UpdateProductBranchPermissionResponse(
                    permission.ProductBranchPermissionId,
                    permission.ProductId,
                    permission.BranchId));
        }
    }
}