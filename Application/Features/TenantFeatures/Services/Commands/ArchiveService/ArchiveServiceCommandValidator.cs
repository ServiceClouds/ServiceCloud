using FluentValidation;

namespace Application.Features.TenantFeatures.Services.Commands.ArchiveService;

public sealed class ArchiveServiceCommandValidator
    : AbstractValidator<ArchiveServiceCommand>
{
    public ArchiveServiceCommandValidator()
    {
        
    }
}