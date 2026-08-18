using Application.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetServiceCategoryById
{
    public sealed record GetServiceCategoryByIdQuery(
    int ServiceCategoryId
) : IQuery<GetServiceCategoryByIdResponse>;
}