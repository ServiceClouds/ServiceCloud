using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetProductCategoryById
{
    public sealed record GetProductCategoryByIdQuery(
     int ProductCategoryId
 ) : IQuery<ProductCategory>;
}
