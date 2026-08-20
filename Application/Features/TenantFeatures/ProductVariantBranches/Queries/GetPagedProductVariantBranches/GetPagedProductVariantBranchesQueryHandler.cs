using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetPagedProductVariantBranches
{


    public sealed class GetPagedProductVariantBranchesQueryHandler
        : IRequestHandler<
            GetPagedProductVariantBranchesQuery,
            Result<PagedResponse<ProductVariantBranchResponse>>>
    {
        private readonly ITenantRepository<ProductVariantBranch> _repository;

        public GetPagedProductVariantBranchesQueryHandler(
            ITenantRepository<ProductVariantBranch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ProductVariantBranchResponse>>> Handle(
            GetPagedProductVariantBranchesQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                _repository
                    .GetAll()
                    .Where(x => !x.IsArchived);

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                var search = request.Request.Search.Trim();

                query = query.Where(x =>
                    x.Barcode!.Contains(search) ||
                    x.Sku!.Contains(search) ||
                    x.SupplierCode!.Contains(search));
            }

            var totalRecords =
                await query.CountAsync(cancellationToken);

            var branches =
                await query
                    .Skip(
                        (request.Request.PageNumber - 1) *
                        request.Request.PageSize)
                    .Take(request.Request.PageSize)
                    .Select(x => new ProductVariantBranchResponse(
                        x.ProductVariantBranchId,
                        x.ProductVariantId,
                        x.BranchId,
                        x.IsActive,
                        x.IsIncluded,
                        x.Barcode,
                        x.Sku,
                        x.SupplierId,
                        x.SupplierCode,
                        x.ReorderThreshold,
                        x.ReorderQuantity,
                        x.SupplierPrice,
                        x.Price,
                        x.TotalTaxPercentage,
                        x.TotalPrice))
                    .ToListAsync(cancellationToken);

            var totalPages =
                (int)Math.Ceiling(
                    totalRecords /
                    (double)request.Request.PageSize);

            return Result<PagedResponse<ProductVariantBranchResponse>>.Success(
                new PagedResponse<ProductVariantBranchResponse>
                {
                    Items = branches,
                    PageNumber = request.Request.PageNumber,
                    PageSize = request.Request.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages
                });
        }
    }
}