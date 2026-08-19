using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Products.Commands.ArchiveProduct;

public sealed record ArchiveProductCommand(
    int ProductId
) : ICommand;
