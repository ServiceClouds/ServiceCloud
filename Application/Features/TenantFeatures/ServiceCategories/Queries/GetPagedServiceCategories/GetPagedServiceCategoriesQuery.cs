using Application.Abstractions.Queries;
using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetPagedServiceCategories
{
    public sealed record GetPagedServiceCategoriesQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities.ServiceCategory>>;
}
