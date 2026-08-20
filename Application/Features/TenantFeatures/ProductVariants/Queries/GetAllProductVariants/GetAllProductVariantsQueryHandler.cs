using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetAllProductVariants
{


    public sealed class GetAllProductVariantsQueryHandler
        : IRequestHandler<
            GetAllProductVariantsQuery,
            Result<List<ProductVariantResponse>>>
    {
        private readonly ITenantRepository<ProductVariant> _repository;

        public GetAllProductVariantsQueryHandler(
            ITenantRepository<ProductVariant> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductVariantResponse>>> Handle(
            GetAllProductVariantsQuery request,
            CancellationToken cancellationToken)
        {
            var productVariants =
                await _repository
                    .GetAll()
                    .Where(x => !x.IsArchived)
                    .Select(x => new ProductVariantResponse(
                        x.ProductVariantId,
                        x.ProductId,
                        x.ProductVariantName,
                        x.AttributeValueIds,
                        x.IsStandard,
                        x.IsArchived,
                        x.CreatedOn,
                        x.CreatedBy,
                        x.ModifiedOn,
                        x.ModifiedBy,
                        x.SortedAttributeIds,
                        x.SortedAttributeValueIds))
                    .ToListAsync(cancellationToken);

            return Result<List<ProductVariantResponse>>
                .Success(productVariants);
        }
    }
}