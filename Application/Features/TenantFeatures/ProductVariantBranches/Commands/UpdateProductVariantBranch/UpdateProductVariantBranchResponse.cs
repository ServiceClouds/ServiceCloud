using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.UpdateProductVariantBranch
{
    public sealed record UpdateProductVariantBranchResponse(
    long ProductVariantBranchId,
    long ProductVariantId,
    int BranchId
);
}
