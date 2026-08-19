namespace Application.Features.TenantFeatures.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    int ProductId,
    string? ProductName
);
