using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Role")]
[Index("IsActive", Name = "IX_Role_IsActive")]
public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(100)]
    public string RoleName { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<StaffBranch> StaffBranches { get; set; } = new List<StaffBranch>();
}
