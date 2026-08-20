using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetAllProductVariantBranches
{
    public sealed record GetAllProductVariantBranchesQuery
     : IRequest<Result<List<ProductVariantBranchResponse>>>;
}
