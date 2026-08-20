using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetProductBranchPermissionById;


public sealed class GetProductBranchPermissionByIdQueryHandler
    : IRequestHandler<
        GetProductBranchPermissionByIdQuery,
        Result<ProductBranchPermissionResponse>>
{
    private readonly ITenantRepository<ProductBranchPermission> _repository;

    public GetProductBranchPermissionByIdQueryHandler(
        ITenantRepository<ProductBranchPermission> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductBranchPermissionResponse>> Handle(
        GetProductBranchPermissionByIdQuery request,
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
            return Result<ProductBranchPermissionResponse>.Failure(
                Error.NotFound(
                    "Product branch permission was not found."));
        }

        return Result<ProductBranchPermissionResponse>.Success(
            new ProductBranchPermissionResponse(
                permission.ProductBranchPermissionId,
                permission.ProductId,
                permission.BranchId,
                permission.IsActive,
                permission.IsOnline,
                permission.IsHidePriceOnline,
                permission.IsFeatured,
                permission.HasTrackingventory,
                permission.HasShipping,
                permission.IsIncluded,
                permission.BusinessUseOnly,
                permission.IsVariantGenerated,
                permission.IsSharedPrivately));
    }
}