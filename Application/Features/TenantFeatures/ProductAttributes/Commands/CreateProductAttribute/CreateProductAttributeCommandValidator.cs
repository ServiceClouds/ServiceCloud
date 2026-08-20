using FluentValidation;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;

public sealed class CreateProductAttributeCommandValidator
    : AbstractValidator<CreateProductAttributeCommand>
{
    public CreateProductAttributeCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.EAttributeId)
            .GreaterThan(0)
            .WithMessage("EAttributeId must be greater than zero.");

        RuleFor(x => x.SortOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("SortOrder cannot be negative.");
    }
}
