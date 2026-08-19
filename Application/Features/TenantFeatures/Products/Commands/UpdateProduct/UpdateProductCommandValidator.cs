using FluentValidation;

namespace Application.Features.TenantFeatures.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator
    : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.ProductCategoryId)
            .GreaterThan(0);

        RuleFor(x => x.ProductName)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1500);
    }
}
