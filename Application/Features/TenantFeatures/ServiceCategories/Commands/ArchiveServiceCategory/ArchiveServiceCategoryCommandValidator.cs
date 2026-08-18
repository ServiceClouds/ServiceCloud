using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.ArchiveServiceCategory
{
    public sealed class ArchiveServiceCategoryCommandValidator
    : AbstractValidator<ArchiveServiceCategoryCommand>
    {
        public ArchiveServiceCategoryCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryId)
                .GreaterThan(0);
        }
    }
}
