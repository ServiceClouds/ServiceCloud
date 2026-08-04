using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;

[Table("ServiceCategory")]
public partial class ServiceCategory
{
    // Required by EF Core for entity materialization
    private ServiceCategory()
    {
        ServiceCategoryBranches = new List<ServiceCategoryBranch>();
        Services = new List<Service>();
    }

    [Key]
    public int ServiceCategoryId { get; private set; }

    [StringLength(100)]
    public string? ServiceCategoryName { get; private set; }

    [StringLength(500)]
    public string? Description { get; private set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? ImagePath { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public bool IsArchived { get; private set; }

    public int CompanyId { get; private set; }

    public bool HasBranchPermission { get; private set; }

    public int AppSourceTypeId { get; private set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Color { get; private set; }

    public int? SortIndex { get; private set; }

    [InverseProperty("ServiceCategory")]
    public virtual ICollection<ServiceCategoryBranch> ServiceCategoryBranches { get; private set; }

    [InverseProperty("ServiceCategory")]
    public virtual ICollection<Service> Services { get; private set; }

    
    public static ServiceCategory Create(
        string? serviceCategoryName,
        string? description,
        string? imagePath,
        int createdBy,
        int companyId,
        bool hasBranchPermission,
        int appSourceTypeId,
        string? color,
        int? sortIndex)
    {
        return new ServiceCategory
        {
            ServiceCategoryName = serviceCategoryName,
            Description = description,
            ImagePath = imagePath,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,
            CompanyId = companyId,
            HasBranchPermission = hasBranchPermission,
            AppSourceTypeId = appSourceTypeId,
            Color = color,
            SortIndex = sortIndex,
            IsArchived = false // Default status
        };
    }

   
    public void Update(
        string? serviceCategoryName,
        string? description,
        string? imagePath,
        bool hasBranchPermission,
        string? color,
        int? sortIndex,
        int modifiedBy)
    {
        ServiceCategoryName = serviceCategoryName;
        Description = description;
        ImagePath = imagePath;
        HasBranchPermission = hasBranchPermission;
        Color = color;
        SortIndex = sortIndex;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }

    
    public void Archive(int modifiedBy)
    {
        IsArchived = true;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}
