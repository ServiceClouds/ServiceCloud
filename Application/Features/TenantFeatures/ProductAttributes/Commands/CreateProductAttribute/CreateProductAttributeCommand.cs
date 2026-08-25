using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;

public sealed record CreateProductAttributeCommand(
    int productAttributeId,
    int ProductId,
    int EAttributeId,
    int SortOrder
) : ICommand<CreateProductAttributeResponse>;
