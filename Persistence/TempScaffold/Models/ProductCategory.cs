using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? ProductCategoryName { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public bool HasBranchPermission { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsArchived { get; set; }

    public int? CompanyId { get; set; }

    public int? AppSourceTypeId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
