using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory
{
    public sealed class CreateProductCategoryCommandValidator
    : AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductCategoryName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.ImagePath)
                .MaximumLength(500);
        }
    }
}
