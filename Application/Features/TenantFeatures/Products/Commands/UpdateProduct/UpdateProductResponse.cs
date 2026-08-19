namespace Application.Features.TenantFeatures.Products.Commands.UpdateProduct;

public sealed record UpdateProductResponse(
    int ProductId,
    string? ProductName
);
