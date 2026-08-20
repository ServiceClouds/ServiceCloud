using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetAllProductBranchPermissions;

public sealed class GetAllProductBranchPermissionsQueryHandler
    : IRequestHandler<
        GetAllProductBranchPermissionsQuery,
        Result<List<ProductBranchPermissionResponse>>>
{
    private readonly ITenantRepository<ProductBranchPermission> _repository;

    public GetAllProductBranchPermissionsQueryHandler(
        ITenantRepository<ProductBranchPermission> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProductBranchPermissionResponse>>> Handle(
        GetAllProductBranchPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var permissions =
            await _repository
                .GetAll()
                .Select(x => new ProductBranchPermissionResponse(
                    x.ProductBranchPermissionId,
                    x.ProductId,
                    x.BranchId,
                    x.IsActive,
                    x.IsOnline,
                    x.IsHidePriceOnline,
                    x.IsFeatured,
                    x.HasTrackingventory,
                    x.HasShipping,
                    x.IsIncluded,
                    x.BusinessUseOnly,
                    x.IsVariantGenerated,
                    x.IsSharedPrivately))
                .ToListAsync(cancellationToken);

        return Result<List<ProductBranchPermissionResponse>>
            .Success(permissions);
    }
}