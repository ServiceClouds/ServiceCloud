using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.UpdateProductVariantBranch
{
    public sealed class UpdateProductVariantBranchCommandValidator
    : AbstractValidator<UpdateProductVariantBranchCommand>
    {
        public UpdateProductVariantBranchCommandValidator()
        {
            RuleFor(x => x.ProductVariantBranchId)
                .GreaterThan(0);
        }
    }
}
