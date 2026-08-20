using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetProductVariantById
{


    public sealed class GetProductVariantByIdQueryHandler
        : IRequestHandler<
            GetProductVariantByIdQuery,
            Result<ProductVariantResponse>>
    {
        private readonly ITenantRepository<ProductVariant> _repository;

        public GetProductVariantByIdQueryHandler(
            ITenantRepository<ProductVariant> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductVariantResponse>> Handle(
            GetProductVariantByIdQuery request,
            CancellationToken cancellationToken)
        {
            var productVariant =
                await _repository.FirstOrDefaultAsync(
                    x =>
                        x.ProductVariantId == request.ProductVariantId &&
                        !x.IsArchived,
                    cancellationToken: cancellationToken);

            if (productVariant is null)
            {
                return Result<ProductVariantResponse>.Failure(
                    Error.NotFound("Product variant was not found."));
            }

            return Result<ProductVariantResponse>.Success(
                new ProductVariantResponse(
                    productVariant.ProductVariantId,
                    productVariant.ProductId,
                    productVariant.ProductVariantName,
                    productVariant.AttributeValueIds,
                    productVariant.IsStandard,
                    productVariant.IsArchived,
                    productVariant.CreatedOn,
                    productVariant.CreatedBy,
                    productVariant.ModifiedOn,
                    productVariant.ModifiedBy,
                    productVariant.SortedAttributeIds,
                    productVariant.SortedAttributeValueIds));
        }
    }
}