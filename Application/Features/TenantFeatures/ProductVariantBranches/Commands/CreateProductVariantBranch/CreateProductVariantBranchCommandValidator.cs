using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.CreateProductVariantBranch
{
    public sealed class CreateProductVariantBranchCommandValidator
    : AbstractValidator<CreateProductVariantBranchCommand>
    {
        public CreateProductVariantBranchCommandValidator()
        {
            RuleFor(x => x.ProductVariantId)
                .GreaterThan(0);

            RuleFor(x => x.BranchId)
                .GreaterThan(0);
        }
    }
}
