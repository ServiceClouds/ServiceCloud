using FluentValidation;

namespace Application.Features.Services.Commands.CreateService;

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

        RuleFor(x => x.CompanyId)
            .GreaterThan(0);

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0);

        RuleFor(x => x.AppSourceTypeId)
            .GreaterThan(0);
    }
}