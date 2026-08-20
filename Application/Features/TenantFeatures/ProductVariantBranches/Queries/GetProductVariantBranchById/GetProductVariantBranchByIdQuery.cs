using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetProductVariantBranchById
{
    

    public sealed record GetProductVariantBranchByIdQuery(
        long ProductVariantBranchId
    ) : IRequest<Result<ProductVariantBranchResponse>>;
}
