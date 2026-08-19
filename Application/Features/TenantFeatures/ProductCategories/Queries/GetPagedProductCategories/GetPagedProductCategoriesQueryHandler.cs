using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetPagedProductCategories
{

    public sealed class GetPagedProductCategoriesQueryHandler
        : IQueryHandler<
            GetPagedProductCategoriesQuery,
            PagedResponse<ProductCategory>>
    {
        private readonly ITenantRepository<ProductCategory> _repository;

        public GetPagedProductCategoriesQueryHandler(
            ITenantRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ProductCategory>>> Handle(
            GetPagedProductCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .Where(x => !x.IsArchived);

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                var search = request.Request.Search.Trim();

                query = query.Where(x =>
                    (x.ProductCategoryName != null &&
                     x.ProductCategoryName.Contains(search))
                    ||
                    (x.Description != null &&
                     x.Description.Contains(search)));
            }

            var totalRecords =
                await query.CountAsync(cancellationToken);

            var categories = await query
                .OrderBy(x => x.ProductCategoryId)
                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);

            var totalPages =
                request.Request.PageSize > 0
                    ? (int)Math.Ceiling(
                        (double)totalRecords /
                        request.Request.PageSize)
                    : 0;

            var response = new PagedResponse<ProductCategory>
            {
                Items = categories,
                PageNumber = request.Request.PageNumber,
                PageSize = request.Request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };

            return Result<PagedResponse<ProductCategory>>
                .Success(response);
        }
    }
}