using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetAllProductCategories
{

    public sealed record GetAllProductCategoriesQuery
        : IQuery<List<ProductCategory>>;
}
