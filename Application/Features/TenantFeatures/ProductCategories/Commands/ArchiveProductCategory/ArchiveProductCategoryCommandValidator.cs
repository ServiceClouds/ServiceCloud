using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.ArchiveProductCategory
{
    public sealed class ArchiveProductCategoryCommandValidator
    : AbstractValidator<ArchiveProductCategoryCommand>
    {
        public ArchiveProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0);
        }
    }
}
