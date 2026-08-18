

namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductVariant
{
    private ProductVariant()
    {
    }

    public long ProductVariantId { get; private set; }

    public int ProductId { get; private set; }

    public string ProductVariantName { get; private set; } = string.Empty;

    public string? AttributeValueIds { get; private set; }

    public bool IsStandard { get; private set; }

    public bool IsArchived { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public string? SortedAttributeIds { get; private set; }

    public string? SortedAttributeValueIds { get; private set; }

    public Product Product { get; private set; } = null!;

    public ICollection<ProductVariantBranch> ProductVariantBranches { get; private set; }
        = new List<ProductVariantBranch>();

    public ICollection<ProductVariantPackaging> ProductVariantPackagings { get; private set; }
        = new List<ProductVariantPackaging>();

    public static ProductVariant Create(
        int productId,
        string productVariantName,
        int createdBy,
        string? attributeValueIds = null,
        bool isStandard = false,
        string? sortedAttributeIds = null,
        string? sortedAttributeValueIds = null)
    {
        return new ProductVariant
        {
            ProductId = productId,
            ProductVariantName = productVariantName,
            AttributeValueIds = attributeValueIds,
            IsStandard = isStandard,
            IsArchived = false,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,
            SortedAttributeIds = sortedAttributeIds,
            SortedAttributeValueIds = sortedAttributeValueIds
        };
    }

    public void Update(
        string productVariantName,
        int modifiedBy,
        string? attributeValueIds = null,
        bool isStandard = false,
        string? sortedAttributeIds = null,
        string? sortedAttributeValueIds = null)
    {
        ProductVariantName = productVariantName;
        AttributeValueIds = attributeValueIds;
        IsStandard = isStandard;
        SortedAttributeIds = sortedAttributeIds;
        SortedAttributeValueIds = sortedAttributeValueIds;

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