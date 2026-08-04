using FluentValidation;

namespace Application.Features.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandValidator
    : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0);

        RuleFor(x => x.CompanyId)
            .GreaterThan(0);

        RuleFor(x => x.ModifiedBy)
            .GreaterThan(0);

        RuleFor(x => x.ServiceCategoryId)
            .GreaterThan(0);

        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .MaximumLength(100);
    }
}