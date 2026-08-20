using Application.Common;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetPagedProductVariants
{
    public sealed record GetPagedProductVariantsQuery(
    PaginationRequest Request
) : IRequest<Result<PagedResponse<ProductVariantResponse>>>;
}
