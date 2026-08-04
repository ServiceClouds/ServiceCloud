using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TempScaffold.Models;

[Table("ServiceCategory")]
public partial class ServiceCategory
{
    [Key]
    public int ServiceCategoryId { get; set; }
    
    [StringLength(100)]
    public string? ServiceCategoryName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsArchived { get; set; }

    public int CompanyId { get; set; }

    public bool HasBranchPermission { get; set; }

    public int AppSourceTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Color { get; set; }

    public int? SortIndex { get; set; }

    [InverseProperty("ServiceCategory")]
    public virtual ICollection<ServiceCategoryBranch> ServiceCategoryBranches { get; set; } = new List<ServiceCategoryBranch>();

    [InverseProperty("ServiceCategory")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
