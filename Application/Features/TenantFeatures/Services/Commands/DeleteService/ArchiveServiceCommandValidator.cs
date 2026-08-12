using FluentValidation;

namespace Application.Features.TenantFeatures.Services.Commands.DeleteService;

public sealed class ArchiveServiceCommandValidator
    : AbstractValidator<ArchiveServiceCommand>
{
    public ArchiveServiceCommandValidator()
    {
        
    }
}