using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.CreateProductVariantBranch
{
    public sealed record CreateProductVariantBranchResponse(
     long ProductVariantBranchId,
     long ProductVariantId,
     int BranchId
 );
}
