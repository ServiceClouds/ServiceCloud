using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.CreateServiceCategory
{
    public sealed class CreateServiceCategoryCommandValidator
    : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryName)
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.ImagePath)
                .MaximumLength(80);

            RuleFor(x => x.AppSourceTypeId)
                .GreaterThan(0);

            RuleFor(x => x.Color)
                .MaximumLength(50);
        }
    }
}
