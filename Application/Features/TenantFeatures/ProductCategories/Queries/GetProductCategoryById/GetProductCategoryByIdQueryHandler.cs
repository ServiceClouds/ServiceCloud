using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetProductCategoryById
{


    public sealed class GetProductCategoryByIdQueryHandler
        : IQueryHandler<GetProductCategoryByIdQuery, ProductCategory>
    {
        private readonly ITenantRepository<ProductCategory> _repository;

        public GetProductCategoryByIdQueryHandler(
            ITenantRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductCategory>> Handle(
            GetProductCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.FirstOrDefaultAsync(
                x => x.ProductCategoryId == request.ProductCategoryId &&
                     !x.IsArchived,
                true,
                cancellationToken);

            if (category is null)
            {
                return Result<ProductCategory>.Failure(
                    Error.NotFound("Product Category not found."));
            }

            return Result<ProductCategory>.Success(category);
        }
    }
}