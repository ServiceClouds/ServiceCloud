using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.UpdateProductCategory
{
    public sealed record UpdateProductCategoryResponse(
    int ProductCategoryId,
    string? ProductCategoryName);
}
