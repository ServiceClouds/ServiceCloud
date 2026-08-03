using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StaffBranch")]
[Index("BranchId", Name = "IX_StaffBranch_BranchId")]
[Index("CanDoClass", Name = "IX_StaffBranch_CanDoClass")]
[Index("CanDoService", Name = "IX_StaffBranch_CanDoService")]
[Index("IsActive", Name = "IX_StaffBranch_IsActive")]
[Index("RoleId", Name = "IX_StaffBranch_RoleId")]
[Index("StaffId", Name = "IX_StaffBranch_StaffId")]
public partial class StaffBranch
{
    [Key]
    public int StaffBranchId { get; set; }

    public int StaffId { get; set; }

    public int BranchId { get; set; }

    public int? RoleId { get; set; }

    public int? DialerStatusTypeId { get; set; }

    [StringLength(40)]
    public string? OnlineDisplayName { get; set; }

    public bool ShowOnScheduler { get; set; }

    public bool CanDoClass { get; set; }

    public bool CanDoService { get; set; }

    public bool CanDoServiceOnline { get; set; }

    public bool CanDoCourse { get; set; }

    public bool FirstAidAllowed { get; set; }

    public bool AllowTip { get; set; }

    public bool DoorAccessAllowed { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("StaffBranches")]
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("StaffBranches")]
    public virtual Role? Role { get; set; }

    [ForeignKey("StaffId")]
    [InverseProperty("StaffBranches")]
    public virtual Staff Staff { get; set; } = null!;
}
