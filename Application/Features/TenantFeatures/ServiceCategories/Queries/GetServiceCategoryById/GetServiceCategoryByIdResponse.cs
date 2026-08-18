using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetServiceCategoryById
{
    public sealed record GetServiceCategoryByIdResponse(
    int ServiceCategoryId,
    string? ServiceCategoryName,
    string? Description,
    string? ImagePath,
    bool HasBranchPermission,
    int AppSourceTypeId,
    string? Color,
    int? SortIndex,
    bool IsArchived,
    int CompanyId);
}
