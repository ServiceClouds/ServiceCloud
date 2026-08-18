using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.UpdateServiceCategory
{
    public sealed class UpdateServiceCategoryCommandValidator
    : AbstractValidator<UpdateServiceCategoryCommand>
    {
        public UpdateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryId)
                .GreaterThan(0);

            RuleFor(x => x.ServiceCategoryName)
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.ImagePath)
                .MaximumLength(80);

            RuleFor(x => x.Color)
                .MaximumLength(50);
        }
    }
}
