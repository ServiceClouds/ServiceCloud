using Application.Abstractions.Commands;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory
{
    public sealed record CreateProductCategoryCommand(
       int ProductCategoryId,
    string? ProductCategoryName,
    string? Description,
    string? ImagePath,
    bool HasBranchPermission,
    int? AppSourceTypeId
) : ICommand<CreateProductCategoryResponse>;
}
