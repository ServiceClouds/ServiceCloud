using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory
{
    public sealed record CreateProductCategoryCommand(
    string? ProductCategoryName,
    string? Description,
    string? ImagePath,
    bool HasBranchPermission,
    int? AppSourceTypeId
) : ICommand<CreateProductCategoryResponse>;
}
