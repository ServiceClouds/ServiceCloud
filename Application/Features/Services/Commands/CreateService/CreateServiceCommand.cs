using Application.Abstractions.Commands;

namespace Application.Features.Services.Commands.CreateService;

public sealed record CreateServiceCommand(
    int ServiceCategoryId,
    string ServiceName,
    string? Description,
    string? SpecialInstruction,
    bool HasBranchPermission,
    bool AllowBranchEditPrice,
    int AppSourceTypeId
) : ICommand<CreateServiceResponse>;