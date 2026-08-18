namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductVariantPackaging
{
    private ProductVariantPackaging()
    {
    }

    public long ProductVariantPackagingId { get; private set; }

    public long ProductVariantId { get; private set; }

    public decimal? Weight { get; private set; }

    public int? WeightUnitId { get; private set; }

    public int? DimensionUnitId { get; private set; }

    public decimal? Length { get; private set; }

    public decimal? Width { get; private set; }

    public decimal? Height { get; private set; }

    public decimal? SizeVolume { get; private set; }

    public int? SizeVolumeUnitId { get; private set; }

    public ProductVariant ProductVariant { get; private set; } = null!;

    public static ProductVariantPackaging Create(
        long productVariantId,
        decimal? weight = null,
        int? weightUnitId = null,
        int? dimensionUnitId = null,
        decimal? length = null,
        decimal? width = null,
        decimal? height = null,
        decimal? sizeVolume = null,
        int? sizeVolumeUnitId = null)
    {
        return new ProductVariantPackaging
        {
            ProductVariantId = productVariantId,
            Weight = weight,
            WeightUnitId = weightUnitId,
            DimensionUnitId = dimensionUnitId,
            Length = length,
            Width = width,
            Height = height,
            SizeVolume = sizeVolume,
            SizeVolumeUnitId = sizeVolumeUnitId
        };
    }

    public void Update(
        decimal? weight,
        int? weightUnitId,
        int? dimensionUnitId,
        decimal? length,
        decimal? width,
        decimal? height,
        decimal? sizeVolume,
        int? sizeVolumeUnitId)
    {
        Weight = weight;
        WeightUnitId = weightUnitId;
        DimensionUnitId = dimensionUnitId;
        Length = length;
        Width = width;
        Height = height;
        SizeVolume = sizeVolume;
        SizeVolumeUnitId = sizeVolumeUnitId;
    }
}