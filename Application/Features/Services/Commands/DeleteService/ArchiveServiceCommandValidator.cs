using FluentValidation;

namespace Application.Features.Services.Commands.ArchiveService;

public sealed class ArchiveServiceCommandValidator
    : AbstractValidator<ArchiveServiceCommand>
{
    public ArchiveServiceCommandValidator()
    {
        
    }
}