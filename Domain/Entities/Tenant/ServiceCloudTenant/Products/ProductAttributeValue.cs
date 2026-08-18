namespace Domain.Entities.Tenant.ServiceCloudTenant.Products;

public class ProductAttributeValue
{
    private ProductAttributeValue()
    {
    }

    public long ProductAttributeValueId { get; private set; }

    public int ProductAttributeId { get; private set; }

    public int AttributeValueId { get; private set; }

    public ProductAttribute ProductAttribute { get; private set; } = null!;

    public static ProductAttributeValue Create(
        int productAttributeId,
        int attributeValueId)
    {
        return new ProductAttributeValue
        {
            ProductAttributeId = productAttributeId,
            AttributeValueId = attributeValueId
        };
    }

    public void Update(int attributeValueId)
    {
        AttributeValueId = attributeValueId;
    }
}