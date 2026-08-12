using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Services.Commands.UpdateService;

public sealed record UpdateServiceCommand(
    
    int ModifiedBy,
    int ServiceId,
    int ServiceCategoryId,
    string ServiceName,
    string? Description,
    string? SpecialInstruction,
    bool HasBranchPermission,
    bool AllowBranchEditPrice
) : ICommand<UpdateServiceResponse>;