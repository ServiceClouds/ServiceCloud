using FluentValidation;

namespace Application.Features.TenantFeatures.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductCategoryId)
            .GreaterThan(0);

        RuleFor(x => x.ProductName)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1500);
    }
}
