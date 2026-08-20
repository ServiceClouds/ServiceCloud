using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Commands.CreateProductVariantPackaging;

public sealed record CreateProductVariantPackagingCommand(
    long ProductVariantId,
    decimal? Weight = null,
    int? WeightUnitId = null,
    int? DimensionUnitId = null,
    decimal? Length = null,
    decimal? Width = null,
    decimal? Height = null,
    decimal? SizeVolume = null,
    int? SizeVolumeUnitId = null
) : ICommand<CreateProductVariantPackagingResponse>;