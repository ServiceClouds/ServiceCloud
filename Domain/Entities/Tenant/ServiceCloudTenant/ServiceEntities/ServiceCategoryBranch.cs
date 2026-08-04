using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;

[Table("ServiceCategoryBranch")]
public partial class ServiceCategoryBranch
{
    // Required by EF Core for entity materialization
    private ServiceCategoryBranch()
    {
    }

    [Key]
    public int ServiceCategoryBranchId { get; private set; }

    public int ServiceCategoryId { get; private set; }

    public int BranchId { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsIncluded { get; private set; }

    [ForeignKey("ServiceCategoryId")]
    [InverseProperty("ServiceCategoryBranches")]
    public virtual ServiceCategory ServiceCategory { get; private set; } = null!;

    
    public static ServiceCategoryBranch Create(
        int serviceCategoryId,
        int branchId,
        bool isIncluded = true)
    {
        return new ServiceCategoryBranch
        {
            ServiceCategoryId = serviceCategoryId,
            BranchId = branchId,
            IsIncluded = isIncluded,
            IsActive = true // Default state when first mapped
        };
    }

   
    public void UpdateStatus(bool isActive, bool isIncluded)
    {
        IsActive = isActive;
        IsIncluded = isIncluded;
    }

    
    public void SetActivationState(bool isActive)
    {
        IsActive = isActive;
    }
}
