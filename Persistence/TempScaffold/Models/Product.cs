using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductCategoryId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsArchived { get; set; }

    public bool AllowBranchTrackInventory { get; set; }

    public bool HasBranchPermission { get; set; }

    public bool AllowBranchEditPrice { get; set; }

    public int? ProductClassificationId { get; set; }

    public int? BrandId { get; set; }

    public int? AppSourceTypeId { get; set; }

    public int? CompanyId { get; set; }

    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

    public virtual ICollection<ProductBranchPermission> ProductBranchPermissions { get; set; } = new List<ProductBranchPermission>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
