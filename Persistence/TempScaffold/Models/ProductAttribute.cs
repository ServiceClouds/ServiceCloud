using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductAttribute
{
    public int ProductAttributeId { get; set; }

    public int ProductId { get; set; }

    public int EAttributeId { get; set; }

    public int SortOrder { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
}
