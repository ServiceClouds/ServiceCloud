using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetAllProductCategories
{


    public sealed class GetAllProductCategoriesQueryHandler
        : IQueryHandler<GetAllProductCategoriesQuery, List<ProductCategory>>
    {
        private readonly ITenantRepository<ProductCategory> _repository;

        public GetAllProductCategoriesQueryHandler(
            ITenantRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductCategory>>> Handle(
            GetAllProductCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _repository
                .GetAll()
                .Where(x => !x.IsArchived)
                .OrderBy(x => x.ProductCategoryId)
                .ToListAsync(cancellationToken);

            return Result<List<ProductCategory>>.Success(categories);
        }
    }
}
