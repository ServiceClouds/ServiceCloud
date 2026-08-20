using FluentValidation;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.UpdateProductAttribute;

public sealed class UpdateProductAttributeCommandValidator
    : AbstractValidator<UpdateProductAttributeCommand>
{
    public UpdateProductAttributeCommandValidator()
    {
        RuleFor(x => x.ProductAttributeId)
            .GreaterThan(0)
            .WithMessage("ProductAttributeId must be greater than zero.");

        RuleFor(x => x.EAttributeId)
            .GreaterThan(0)
            .WithMessage("EAttributeId must be greater than zero.");

        RuleFor(x => x.SortOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("SortOrder cannot be negative.");
    }
}
