using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.CreateServiceCategory
{
    public sealed record CreateServiceCategoryResponse(
    int ServiceCategoryId,
    string? ServiceCategoryName);
}
