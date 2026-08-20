using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.CreateProductVariant
{
    public sealed class CreateProductVariantCommandValidator
    : AbstractValidator<CreateProductVariantCommand>
    {
        public CreateProductVariantCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.ProductVariantName)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
