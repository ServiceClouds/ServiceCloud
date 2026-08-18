using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.UpdateServiceCategory
{
    public sealed record UpdateServiceCategoryCommand(
     int ServiceCategoryId,
     string? ServiceCategoryName,
     string? Description,
     string? ImagePath,
     bool HasBranchPermission,
     string? Color,
     int? SortIndex
 ) : ICommand<UpdateServiceCategoryResponse>;
}
