using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StaffBranch")]
public partial class StaffBranch
{
    private StaffBranch()
    {
    }

    [Key]
    public int StaffBranchId { get; private set; }

    public int StaffId { get; private set; }

    public int BranchId { get; private set; }

    public int? RoleId { get; private set; }

    public int? DialerStatusTypeId { get; private set; }

    [StringLength(40)]
    public string? OnlineDisplayName { get; private set; }

    public bool ShowOnScheduler { get; private set; }

    public bool CanDoClass { get; private set; }

    public bool CanDoService { get; private set; }

    public bool CanDoServiceOnline { get; private set; }

    public bool CanDoCourse { get; private set; }

    public bool FirstAidAllowed { get; private set; }

    public bool AllowTip { get; private set; }

    public bool DoorAccessAllowed { get; private set; }

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    [ForeignKey("BranchId")]
    [InverseProperty("StaffBranches")]
    public virtual Branch Branch { get; private set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("StaffBranches")]
    public virtual Role? Role { get; private set; }

    [ForeignKey("StaffId")]
    [InverseProperty("StaffBranches")]
    public virtual Staff Staff { get; private set; } = null!;


    public static StaffBranch Create(
        int staffId,
        int branchId,
        int? roleId,
        int? dialerStatusTypeId,
        string? onlineDisplayName,
        bool showOnScheduler,
        bool canDoClass,
        bool canDoService,
        bool canDoServiceOnline,
        bool canDoCourse,
        bool firstAidAllowed,
        bool allowTip,
        bool doorAccessAllowed,
        int createdBy)
    {
        return new StaffBranch
        {
            StaffId = staffId,
            BranchId = branchId,
            RoleId = roleId,
            DialerStatusTypeId = dialerStatusTypeId,
            OnlineDisplayName = onlineDisplayName,
            ShowOnScheduler = showOnScheduler,
            CanDoClass = canDoClass,
            CanDoService = canDoService,
            CanDoServiceOnline = canDoServiceOnline,
            CanDoCourse = canDoCourse,
            FirstAidAllowed = firstAidAllowed,
            AllowTip = allowTip,
            DoorAccessAllowed = doorAccessAllowed,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        int branchId,
        int? roleId,
        int? dialerStatusTypeId,
        string? onlineDisplayName,
        bool showOnScheduler,
        bool canDoClass,
        bool canDoService,
        bool canDoServiceOnline,
        bool canDoCourse,
        bool firstAidAllowed,
        bool allowTip,
        bool doorAccessAllowed,
        int modifiedBy)
    {
        BranchId = branchId;
        RoleId = roleId;
        DialerStatusTypeId = dialerStatusTypeId;
        OnlineDisplayName = onlineDisplayName;
        ShowOnScheduler = showOnScheduler;
        CanDoClass = canDoClass;
        CanDoService = canDoService;
        CanDoServiceOnline = canDoServiceOnline;
        CanDoCourse = canDoCourse;
        FirstAidAllowed = firstAidAllowed;
        AllowTip = allowTip;
        DoorAccessAllowed = doorAccessAllowed;

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