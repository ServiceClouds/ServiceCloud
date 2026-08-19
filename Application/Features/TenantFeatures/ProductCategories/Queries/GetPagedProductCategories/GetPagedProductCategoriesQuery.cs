using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Queries.GetPagedProductCategories
{
    public sealed record GetPagedProductCategoriesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<ProductCategory>>;
}
