using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductAttributeValue
{
    public long ProductAttributeValueId { get; set; }

    public int ProductAttributeId { get; set; }

    public int AttributeValueId { get; set; }

    public virtual ProductAttribute ProductAttribute { get; set; } = null!;
}
