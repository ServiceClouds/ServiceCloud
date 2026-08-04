using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TempScaffold.Models;

[Table("ServiceCategoryBranch")]
public partial class ServiceCategoryBranch
{
    [Key]
    public int ServiceCategoryBranchId { get; set; }

    public int ServiceCategoryId { get; set; }

    public int BranchId { get; set; }

    public bool IsActive { get; set; }

    public bool IsIncluded { get; set; }

    [ForeignKey("ServiceCategoryId")]
    [InverseProperty("ServiceCategoryBranches")]
    public virtual ServiceCategory ServiceCategory { get; set; } = null!;
}
