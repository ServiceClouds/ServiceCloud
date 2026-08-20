using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Application.Features.TenantFeatures.ProductBranchPermissions;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetPagedProductBranchPermissions
{


    public sealed class GetPagedProductBranchPermissionsQueryHandler
        : IRequestHandler<
            GetPagedProductBranchPermissionsQuery,
            Result<PagedResponse<ProductBranchPermissionResponse>>>
    {
        private readonly ITenantRepository<ProductBranchPermission> _repository;

        public GetPagedProductBranchPermissionsQueryHandler(
            ITenantRepository<ProductBranchPermission> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ProductBranchPermissionResponse>>> Handle(
            GetPagedProductBranchPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Request.Search) &&
                int.TryParse(
                    request.Request.Search.Trim(),
                    out var searchId))
            {
                query = query.Where(x =>
                    x.ProductBranchPermissionId == searchId ||
                    x.ProductId == searchId ||
                    x.BranchId == searchId);
            }

            var totalRecords =
                await query.CountAsync(cancellationToken);

            var permissions =
                await query
                    .Skip(
                        (request.Request.PageNumber - 1) *
                        request.Request.PageSize)
                    .Take(request.Request.PageSize)
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

            var totalPages =
                (int)Math.Ceiling(
                    totalRecords /
                    (double)request.Request.PageSize);

            return Result<PagedResponse<ProductBranchPermissionResponse>>.Success(
     new PagedResponse<ProductBranchPermissionResponse>
     {
         Items = permissions,
         PageNumber = request.Request.PageNumber,
         PageSize = request.Request.PageSize,
         TotalRecords = totalRecords,
         TotalPages = totalPages
     });
        }
    }
}