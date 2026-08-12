using FluentValidation;

namespace Application.Features.TenantFeatures.Services.Commands.CreateService;

public sealed class CreateServiceCommandValidator
    : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ServiceCategoryId)
            .GreaterThan(0);

       

        RuleFor(x => x.AppSourceTypeId)
            .GreaterThan(0);
    }
}