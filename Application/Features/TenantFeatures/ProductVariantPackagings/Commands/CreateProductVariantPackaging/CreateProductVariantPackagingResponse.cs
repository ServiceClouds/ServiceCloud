namespace Application.Features.TenantFeatures.ProductVariantPackagings.Commands.CreateProductVariantPackaging;

public sealed record CreateProductVariantPackagingResponse(
    long ProductVariantPackagingId,
    long ProductVariantId);