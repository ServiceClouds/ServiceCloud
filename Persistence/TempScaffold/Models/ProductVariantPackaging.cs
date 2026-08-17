using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductVariantPackaging
{
    public long ProductVariantPackagingId { get; set; }

    public long ProductVariantId { get; set; }

    public decimal? Weight { get; set; }

    public int? WeightUnitId { get; set; }

    public int? DimensionUnitId { get; set; }

    public decimal? Length { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }

    public decimal? SizeVolume { get; set; }

    public int? SizeVolumeUnitId { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
