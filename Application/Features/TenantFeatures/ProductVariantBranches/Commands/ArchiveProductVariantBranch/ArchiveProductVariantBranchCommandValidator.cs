using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.ArchiveProductVariantBranch
{
    public sealed class ArchiveProductVariantBranchCommandValidator
    : AbstractValidator<ArchiveProductVariantBranchCommand>
    {
        public ArchiveProductVariantBranchCommandValidator()
        {
            RuleFor(x => x.ProductVariantBranchId)
                .GreaterThan(0);
        }
    }
}
