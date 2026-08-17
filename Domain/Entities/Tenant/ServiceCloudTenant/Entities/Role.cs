using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Role")]
public partial class Role
{
    private Role()
    {
    }

    [Key]
    public int RoleId { get; private set; }

    [StringLength(100)]
    public string RoleName { get; private set; } = null!;

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    [InverseProperty("Role")]
    public virtual ICollection<StaffBranch> StaffBranches { get; private set; }
        = new List<StaffBranch>();


    public static Role Create(
        string roleName,
        int createdBy)
    {
        return new Role
        {
            RoleName = roleName,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        string roleName,
        int modifiedBy)
    {
        RoleName = roleName;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }


    public void Archive(int modifiedBy)
    {
        IsActive = false;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}