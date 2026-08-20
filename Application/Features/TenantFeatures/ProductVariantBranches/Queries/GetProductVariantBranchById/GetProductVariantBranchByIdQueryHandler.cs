using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetProductVariantBranchById
{

    public sealed class GetProductVariantBranchByIdQueryHandler
        : IRequestHandler<
            GetProductVariantBranchByIdQuery,
            Result<ProductVariantBranchResponse>>
    {
        private readonly ITenantRepository<ProductVariantBranch> _repository;

        public GetProductVariantBranchByIdQueryHandler(
            ITenantRepository<ProductVariantBranch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductVariantBranchResponse>> Handle(
            GetProductVariantBranchByIdQuery request,
            CancellationToken cancellationToken)
        {
            var branch =
                await _repository.FirstOrDefaultAsync(
                    x =>
                        x.ProductVariantBranchId ==
                            request.ProductVariantBranchId &&
                        !x.IsArchived,
                    cancellationToken: cancellationToken);

            if (branch is null)
            {
                return Result<ProductVariantBranchResponse>.Failure(
                    Error.NotFound(
                        "Product variant branch was not found."));
            }

            return Result<ProductVariantBranchResponse>.Success(
                new ProductVariantBranchResponse(
                    branch.ProductVariantBranchId,
                    branch.ProductVariantId,
                    branch.BranchId,
                    branch.IsActive,
                    branch.IsIncluded,
                    branch.Barcode,
                    branch.Sku,
                    branch.SupplierId,
                    branch.SupplierCode,
                    branch.ReorderThreshold,
                    branch.ReorderQuantity,
                    branch.SupplierPrice,
                    branch.Price,
                    branch.TotalTaxPercentage,
                    branch.TotalPrice,
                    branch.IsArchived,
                    branch.CreatedOn,
                    branch.CreatedBy,
                    branch.ModifiedOn,
                    branch.ModifiedBy));
        }
    }
}