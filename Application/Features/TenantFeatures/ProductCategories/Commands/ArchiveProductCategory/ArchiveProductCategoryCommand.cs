using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands;
namespace Application.Features.TenantFeatures.ProductCategories.Commands.ArchiveProductCategory
{
    public sealed record ArchiveProductCategoryCommand(
    int ProductCategoryId
) : ICommand;
}
