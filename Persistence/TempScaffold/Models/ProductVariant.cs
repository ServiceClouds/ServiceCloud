using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductVariant
{
    public long ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public string ProductVariantName { get; set; } = null!;

    public string? AttributeValueIds { get; set; }

    public bool IsStandard { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public string? SortedAttributeIds { get; set; }

    public string? SortedAttributeValueIds { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductVariantBranch> ProductVariantBranches { get; set; } = new List<ProductVariantBranch>();

    public virtual ICollection<ProductVariantPackaging> ProductVariantPackagings { get; set; } = new List<ProductVariantPackaging>();
}
