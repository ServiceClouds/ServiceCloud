using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetPagedProductVariants
{


    public sealed class GetPagedProductVariantsQueryHandler
        : IRequestHandler<
            GetPagedProductVariantsQuery,
            Result<PagedResponse<ProductVariantResponse>>>
    {
        private readonly ITenantRepository<ProductVariant> _repository;

        public GetPagedProductVariantsQueryHandler(
            ITenantRepository<ProductVariant> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ProductVariantResponse>>> Handle(
            GetPagedProductVariantsQuery request,
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
                    x.ProductVariantName.Contains(search) ||
                    x.AttributeValueIds!.Contains(search) ||
                    x.SortedAttributeIds!.Contains(search) ||
                    x.SortedAttributeValueIds!.Contains(search));
            }

            var totalRecords =
                await query.CountAsync(cancellationToken);

            var productVariants =
                await query
                    .Skip(
                        (request.Request.PageNumber - 1) *
                        request.Request.PageSize)
                    .Take(request.Request.PageSize)
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

            var totalPages =
                (int)Math.Ceiling(
                    totalRecords /
                    (double)request.Request.PageSize);

            return Result<PagedResponse<ProductVariantResponse>>.Success(
                new PagedResponse<ProductVariantResponse>
                {
                    Items = productVariants,
                    PageNumber = request.Request.PageNumber,
                    PageSize = request.Request.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages
                });
        }
    }
}