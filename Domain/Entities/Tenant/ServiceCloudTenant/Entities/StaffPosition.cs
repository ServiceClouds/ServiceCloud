using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StaffPosition")]
[Index("IsActive", Name = "IX_StaffPosition_IsActive")]
public partial class StaffPosition
{
    [Key]
    public int StaffPositionId { get; set; }

    [StringLength(100)]
    public string PositionName { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [InverseProperty("StaffPosition")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
