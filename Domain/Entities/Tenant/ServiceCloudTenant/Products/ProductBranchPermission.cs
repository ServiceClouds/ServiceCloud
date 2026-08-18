namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductBranchPermission
{
    private ProductBranchPermission()
    {
    }

    public int ProductBranchPermissionId { get; private set; }

    public int ProductId { get; private set; }

    public int BranchId { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsOnline { get; private set; }

    public bool IsHidePriceOnline { get; private set; }

    public bool IsFeatured { get; private set; }

    public bool HasTrackingventory { get; private set; }

    public bool HasShipping { get; private set; }

    public bool IsIncluded { get; private set; }

    public bool BusinessUseOnly { get; private set; }

    public bool IsVariantGenerated { get; private set; }

    public bool IsSharedPrivately { get; private set; }

    public Product Product { get; private set; } = null!;

    public static ProductBranchPermission Create(
        int productId,
        int branchId,
        bool isActive = true,
        bool isOnline = false,
        bool isHidePriceOnline = false,
        bool isFeatured = false,
        bool hasTrackingventory = false,
        bool hasShipping = false,
        bool isIncluded = false,
        bool businessUseOnly = false,
        bool isVariantGenerated = false,
        bool isSharedPrivately = false)
    {
        return new ProductBranchPermission
        {
            ProductId = productId,
            BranchId = branchId,
            IsActive = isActive,
            IsOnline = isOnline,
            IsHidePriceOnline = isHidePriceOnline,
            IsFeatured = isFeatured,
            HasTrackingventory = hasTrackingventory,
            HasShipping = hasShipping,
            IsIncluded = isIncluded,
            BusinessUseOnly = businessUseOnly,
            IsVariantGenerated = isVariantGenerated,
            IsSharedPrivately = isSharedPrivately
        };
    }

    public void Update(
        bool isActive,
        bool isOnline,
        bool isHidePriceOnline,
        bool isFeatured,
        bool hasTrackingventory,
        bool hasShipping,
        bool isIncluded,
        bool businessUseOnly,
        bool isVariantGenerated,
        bool isSharedPrivately)
    {
        IsActive = isActive;
        IsOnline = isOnline;
        IsHidePriceOnline = isHidePriceOnline;
        IsFeatured = isFeatured;
        HasTrackingventory = hasTrackingventory;
        HasShipping = hasShipping;
        IsIncluded = isIncluded;
        BusinessUseOnly = businessUseOnly;
        IsVariantGenerated = isVariantGenerated;
        IsSharedPrivately = isSharedPrivately;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}