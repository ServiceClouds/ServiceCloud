

namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class Product
{
    private Product()
    {
    }

    public int ProductId { get; private set; }

    public int ProductCategoryId { get; private set; }

    public string? ProductName { get; private set; }

    public string? Description { get; private set; }

    public bool? IsActive { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public bool? IsArchived { get; private set; }

    public bool AllowBranchTrackInventory { get; private set; }

    public bool HasBranchPermission { get; private set; }

    public bool AllowBranchEditPrice { get; private set; }

    public int? ProductClassificationId { get; private set; }

    public int? BrandId { get; private set; }

    public int? AppSourceTypeId { get; private set; }

    public int? CompanyId { get; private set; }

    public ICollection<ProductAttribute> ProductAttributes { get; private set; }
        = new List<ProductAttribute>();

    public ICollection<ProductBranchPermission> ProductBranchPermissions { get; private set; }
        = new List<ProductBranchPermission>();

    public ProductCategory ProductCategory { get; private set; } = null!;

    public ICollection<ProductVariant> ProductVariants { get; private set; }
        = new List<ProductVariant>();

    public static Product Create(
        int productCategoryId,
        int createdBy,
        string? productName = null,
        string? description = null,
        bool? isActive = true,
        bool? isArchived = false,
        bool allowBranchTrackInventory = false,
        bool hasBranchPermission = false,
        bool allowBranchEditPrice = false,
        int? productClassificationId = null,
        int? brandId = null,
        int? appSourceTypeId = null,
        int? companyId = null)
    {
        return new Product
        {
            ProductCategoryId = productCategoryId,
            ProductName = productName,
            Description = description,
            IsActive = isActive,
            IsArchived = isArchived,
            AllowBranchTrackInventory = allowBranchTrackInventory,
            HasBranchPermission = hasBranchPermission,
            AllowBranchEditPrice = allowBranchEditPrice,
            ProductClassificationId = productClassificationId,
            BrandId = brandId,
            AppSourceTypeId = appSourceTypeId,
            CompanyId = companyId,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }

    public void Update(
        int productCategoryId,
        int modifiedBy,
        string? productName = null,
        string? description = null,
        bool? isActive = true,
        bool allowBranchTrackInventory = false,
        bool hasBranchPermission = false,
        bool allowBranchEditPrice = false,
        int? productClassificationId = null,
        int? brandId = null,
        int? appSourceTypeId = null,
        int? companyId = null)
    {
        ProductCategoryId = productCategoryId;
        ProductName = productName;
        Description = description;
        IsActive = isActive;
        AllowBranchTrackInventory = allowBranchTrackInventory;
        HasBranchPermission = hasBranchPermission;
        AllowBranchEditPrice = allowBranchEditPrice;
        ProductClassificationId = productClassificationId;
        BrandId = brandId;
        AppSourceTypeId = appSourceTypeId;
        CompanyId = companyId;

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