using FluentValidation;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.UpdateProductAttributeValue;

public sealed class UpdateProductAttributeValueCommandValidator
    : AbstractValidator<UpdateProductAttributeValueCommand>
{
    public UpdateProductAttributeValueCommandValidator()
    {
        RuleFor(x => x.ProductAttributeValueId)
            .GreaterThan(0);

        RuleFor(x => x.AttributeValueId)
            .GreaterThan(0);
    }
}
