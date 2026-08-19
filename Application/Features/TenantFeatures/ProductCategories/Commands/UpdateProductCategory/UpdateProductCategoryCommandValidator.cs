using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.UpdateProductCategory
{
    public sealed class UpdateProductCategoryCommandValidator
    : AbstractValidator<UpdateProductCategoryCommand>
    {
        public UpdateProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0);

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
