namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductCategory
{
    private ProductCategory()
    {
    }

    public int ProductCategoryId { get; private set; }

    public string? ProductCategoryName { get; private set; }

    public string? Description { get; private set; }

    public string? ImagePath { get; private set; }

    public bool HasBranchPermission { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public bool IsArchived { get; private set; }

    public int? CompanyId { get; private set; }

    public int? AppSourceTypeId { get; private set; }

    public ICollection<Product> Products { get; private set; }
        = new List<Product>();

    public static ProductCategory Create(
        int createdBy,
        string? productCategoryName = null,
        string? description = null,
        string? imagePath = null,
        bool hasBranchPermission = false,
        bool isArchived = false,
        int? companyId = null,
        int? appSourceTypeId = null)
    {
        return new ProductCategory
        {
            ProductCategoryName = productCategoryName,
            Description = description,
            ImagePath = imagePath,
            HasBranchPermission = hasBranchPermission,
            IsArchived = isArchived,
            CompanyId = companyId,
            AppSourceTypeId = appSourceTypeId,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }

    public void Update(
        int modifiedBy,
        string? productCategoryName = null,
        string? description = null,
        string? imagePath = null,
        bool hasBranchPermission = false,
        int? companyId = null,
        int? appSourceTypeId = null)
    {
        ProductCategoryName = productCategoryName;
        Description = description;
        ImagePath = imagePath;
        HasBranchPermission = hasBranchPermission;
        CompanyId = companyId;
        AppSourceTypeId = appSourceTypeId;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }

    public void Archive(int modifiedBy)
    {
        IsArchived = true;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}