using FluentValidation;

namespace Application.Features.TenantFeatures.Products.Commands.ArchiveProduct;

public sealed class ArchiveProductCommandValidator
    : AbstractValidator<ArchiveProductCommand>
{
    public ArchiveProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);
    }
}
