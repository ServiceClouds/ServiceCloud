using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StaffPosition")]
public partial class StaffPosition
{
    private StaffPosition()
    {
    }

    [Key]
    public int StaffPositionId { get; private set; }

    [StringLength(100)]
    public string PositionName { get; private set; } = null!;

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    [InverseProperty("StaffPosition")]
    public virtual ICollection<Staff> Staff { get; private set; }
        = new List<Staff>();


    public static StaffPosition Create(
        string positionName,
        int createdBy)
    {
        return new StaffPosition
        {
            PositionName = positionName,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        string positionName,
        int modifiedBy)
    {
        PositionName = positionName;

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