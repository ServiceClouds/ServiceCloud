using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TempScaffold.Models;

[Table("Service")]
public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    public int ServiceCategoryId { get; set; }

    [StringLength(100)]
    public string? ServiceName { get; set; }

    [StringLength(100)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsArchived { get; set; }

    public int CompanyId { get; set; }

    [StringLength(1000)]
    public string? SpecialInstruction { get; set; }

    public bool HasBranchPermission { get; set; }

    public bool AllowBranchEditPrice { get; set; }

    public int AppSourceTypeId { get; set; }

    [ForeignKey("ServiceCategoryId")]
    [InverseProperty("Services")]
    public virtual ServiceCategory ServiceCategory { get; set; } = null!;
}
