using Application.Common;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetPagedProductVariantBranches
{
    public sealed record GetPagedProductVariantBranchesQuery(
     PaginationRequest Request
 ) : IRequest<Result<PagedResponse<ProductVariantBranchResponse>>>;
}
