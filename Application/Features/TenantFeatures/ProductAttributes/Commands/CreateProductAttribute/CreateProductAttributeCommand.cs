using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;

public sealed record CreateProductAttributeCommand(
    int ProductId,
    int EAttributeId,
    int SortOrder
) : ICommand<CreateProductAttributeResponse>;
