

namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductAttribute
{
    private ProductAttribute()
    {
    }

    public int ProductAttributeId { get; private set; }

    public int ProductId { get; private set; }

    public int EAttributeId { get; private set; }

    public int SortOrder { get; private set; }

    public Product Product { get; private set; } = null!;

    public ICollection<ProductAttributeValue> ProductAttributeValues { get; private set; }
        = new List<ProductAttributeValue>();

    public static ProductAttribute Create(
        int productId,
        int eAttributeId,
        int sortOrder)
    {
        return new ProductAttribute
        {
            ProductId = productId,
            EAttributeId = eAttributeId,
            SortOrder = sortOrder
        };
    }

    public void Update(
        int eAttributeId,
        int sortOrder)
    {
        EAttributeId = eAttributeId;
        SortOrder = sortOrder;
    }
}