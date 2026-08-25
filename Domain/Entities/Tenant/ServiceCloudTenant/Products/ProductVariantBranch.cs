namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductVariantBranch
{
    private ProductVariantBranch()
    {
    }

    public long ProductVariantBranchId { get; private set; }

    public long ProductVariantId { get; private set; }

    public int BranchId { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsIncluded { get; private set; }

    public string? Barcode { get; private set; }

    public string? Sku { get; private set; }

    public int? SupplierId { get; private set; }

    public string? SupplierCode { get; private set; }

    public int? ReorderThreshold { get; private set; }

    public int? ReorderQuantity { get; private set; }

    public decimal? SupplierPrice { get; private set; }

    public decimal Price { get; private set; }

    public decimal TotalTaxPercentage { get; private set; }

    public decimal TotalPrice { get; private set; }

    public bool IsArchived { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public ProductVariant ProductVariant { get; private set; } = null!;

    public static ProductVariantBranch Create(
        long productVariantbranchId,
        long productVariantId,
        int branchId,
        int createdBy,
        bool isActive = true,
        bool isIncluded = true,
        string? barcode = null,
        string? sku = null,
        int? supplierId = null,
        string? supplierCode = null,
        int? reorderThreshold = null,
        int? reorderQuantity = null,
        decimal? supplierPrice = null,
        decimal price = 0,
        decimal totalTaxPercentage = 0,
        decimal totalPrice = 0)
    {
        return new ProductVariantBranch
        {ProductVariantBranchId=productVariantbranchId,
            ProductVariantId = productVariantId,
            BranchId = branchId,
            IsActive = isActive,
            IsIncluded = isIncluded,
            Barcode = barcode,
            Sku = sku,
            SupplierId = supplierId,
            SupplierCode = supplierCode,
            ReorderThreshold = reorderThreshold,
            ReorderQuantity = reorderQuantity,
            SupplierPrice = supplierPrice,
            Price = price,
            TotalTaxPercentage = totalTaxPercentage,
            TotalPrice = totalPrice,
            IsArchived = false,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }

    public void Update(
        int modifiedBy,
        bool isActive,
        bool isIncluded,
        string? barcode,
        string? sku,
        int? supplierId,
        string? supplierCode,
        int? reorderThreshold,
        int? reorderQuantity,
        decimal? supplierPrice,
        decimal price,
        decimal totalTaxPercentage,
        decimal totalPrice)
    {
        IsActive = isActive;
        IsIncluded = isIncluded;
        Barcode = barcode;
        Sku = sku;
        SupplierId = supplierId;
        SupplierCode = supplierCode;
        ReorderThreshold = reorderThreshold;
        ReorderQuantity = reorderQuantity;
        SupplierPrice = supplierPrice;
        Price = price;
        TotalTaxPercentage = totalTaxPercentage;
        TotalPrice = totalPrice;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }

    public void Archive(int modifiedBy)
    {
        IsArchived = true;
        IsActive = false;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}