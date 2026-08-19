using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.UpdateProductCategory
{
    public sealed record UpdateProductCategoryCommand(
    int ProductCategoryId,
    string? ProductCategoryName,
    string? Description,
    string? ImagePath,
    bool HasBranchPermission,
    int? AppSourceTypeId
) : ICommand<UpdateProductCategoryResponse>;
}
