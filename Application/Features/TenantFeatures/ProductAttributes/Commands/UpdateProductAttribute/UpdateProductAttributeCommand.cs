using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.UpdateProductAttribute;

public sealed record UpdateProductAttributeCommand(
    int ProductAttributeId,
    int EAttributeId,
    int SortOrder
) : ICommand<UpdateProductAttributeResponse>;
