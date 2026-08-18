using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.TenantFeatures.ServiceCategories.Commands.CreateServiceCategory
{
    public sealed record CreateServiceCategoryCommand(
    string? ServiceCategoryName,
    string? Description,
    string? ImagePath,
    bool HasBranchPermission,
    int AppSourceTypeId,
    string? Color,
    int? SortIndex
) : ICommand<CreateServiceCategoryResponse>;
}
