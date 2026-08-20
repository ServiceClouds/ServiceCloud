using FluentValidation;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.CreateProductAttributeValue;

public sealed class CreateProductAttributeValueCommandValidator
    : AbstractValidator<CreateProductAttributeValueCommand>
{
    public CreateProductAttributeValueCommandValidator()
    {
        RuleFor(x => x.ProductAttributeId)
            .GreaterThan(0);

        RuleFor(x => x.AttributeValueId)
            .GreaterThan(0);
    }
}
