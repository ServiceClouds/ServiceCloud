using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetAllProductVariantBranches
{

    public sealed class GetAllProductVariantBranchesQueryHandler
        : IRequestHandler<
            GetAllProductVariantBranchesQuery,
            Result<List<ProductVariantBranchResponse>>>
    {
        private readonly ITenantRepository<ProductVariantBranch> _repository;

        public GetAllProductVariantBranchesQueryHandler(
            ITenantRepository<ProductVariantBranch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductVariantBranchResponse>>> Handle(
            GetAllProductVariantBranchesQuery request,
            CancellationToken cancellationToken)
        {
            var branches =
                await _repository
                    .GetAll()
                    .Where(x => !x.IsArchived)
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

            return Result<List<ProductVariantBranchResponse>>
                .Success(branches);
        }
    }
}