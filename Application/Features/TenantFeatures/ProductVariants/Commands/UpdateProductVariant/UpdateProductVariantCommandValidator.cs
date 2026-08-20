using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.UpdateProductVariant
{
    public sealed class UpdateProductVariantCommandValidator
    : AbstractValidator<UpdateProductVariantCommand>
    {
        public UpdateProductVariantCommandValidator()
        {
            RuleFor(x => x.ProductVariantId)
                .GreaterThan(0);

            RuleFor(x => x.ProductVariantName)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
    }
