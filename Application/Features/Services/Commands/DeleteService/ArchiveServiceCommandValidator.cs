using FluentValidation;

namespace Application.Features.Services.Commands.ArchiveService;

public sealed class ArchiveServiceCommandValidator
    : AbstractValidator<ArchiveServiceCommand>
{
    public ArchiveServiceCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0);

        RuleFor(x => x.ServiceId)
            .GreaterThan(0);

        RuleFor(x => x.ModifiedBy)
            .GreaterThan(0);
    }
}