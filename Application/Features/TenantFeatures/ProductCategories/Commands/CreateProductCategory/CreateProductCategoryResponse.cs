using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory
{
    public sealed record CreateProductCategoryResponse(
    int ProductCategoryId,
    string? ProductCategoryName);
}
