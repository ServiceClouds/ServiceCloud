using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.ArchiveProductVariantBranch
{
    public sealed record ArchiveProductVariantBranchCommand(
    long ProductVariantBranchId
) : ICommand;
}
