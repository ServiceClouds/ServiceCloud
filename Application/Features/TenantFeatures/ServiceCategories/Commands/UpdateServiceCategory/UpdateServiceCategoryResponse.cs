using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.UpdateServiceCategory
{
    public sealed record UpdateServiceCategoryResponse(
    int ServiceCategoryId,
    string? ServiceCategoryName);
}
