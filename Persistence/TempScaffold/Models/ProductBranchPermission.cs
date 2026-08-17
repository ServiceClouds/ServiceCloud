using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductBranchPermission
{
    public int ProductBranchPermissionId { get; set; }

    public int ProductId { get; set; }

    public int BranchId { get; set; }

    public bool IsActive { get; set; }

    public bool IsOnline { get; set; }

    public bool IsHidePriceOnline { get; set; }

    public bool IsFeatured { get; set; }

    public bool HasTrackingventory { get; set; }

    public bool HasShipping { get; set; }

    public bool IsIncluded { get; set; }

    public bool BusinessUseOnly { get; set; }

    public bool IsVariantGenerated { get; set; }

    public bool IsSharedPrivately { get; set; }

    public virtual Product Product { get; set; } = null!;
}
