using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.ArchiveProductVariant
{
    public sealed class ArchiveProductVariantCommandValidator
     : AbstractValidator<ArchiveProductVariantCommand>
    {
        public ArchiveProductVariantCommandValidator()
        {
            RuleFor(x => x.ProductVariantId)
                .GreaterThan(0);
        }
    }
}
