using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;

[Table("Service")]
public partial class Service
{
    // EF Core requires a parameterless constructor when materializing entities from the DB
    private Service()
    {
    }

    [Key]
    public int ServiceId { get; private set; }

    public int ServiceCategoryId { get; private set; }

    [StringLength(100)]
    public string? ServiceName { get; private set; }

    [StringLength(100)]
    public string? Description { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public bool IsArchived { get; private set; }

    public int CompanyId { get; private set; }

    [StringLength(1000)]
    public string? SpecialInstruction { get; private set; }

    public bool HasBranchPermission { get; private set; }

    public bool AllowBranchEditPrice { get; private set; }

    public int AppSourceTypeId { get; private set; }

    [ForeignKey("ServiceCategoryId")]
    [InverseProperty("Services")]
    public virtual ServiceCategory ServiceCategory { get; private set; } = null!;

    /// <summary>
    /// Static Factory Method to create a new Service safely with valid initial state.
    /// </summary>
    public static Service Create(
        int serviceCategoryId,
        string? serviceName,
        string? description,
        int createdBy,
        int companyId,
        string? specialInstruction,
        bool hasBranchPermission,
        bool allowBranchEditPrice,
        int appSourceTypeId)
    {
        return new Service
        {
            ServiceCategoryId = serviceCategoryId,
            ServiceName = serviceName,
            Description = description,
            CreatedOn = DateTime.UtcNow, // Ensures consistent timestamp assignment
            CreatedBy = createdBy,
            CompanyId = companyId,
            SpecialInstruction = specialInstruction,
            HasBranchPermission = hasBranchPermission,
            AllowBranchEditPrice = allowBranchEditPrice,
            AppSourceTypeId = appSourceTypeId,
            IsArchived = false // Default active state for new entities
        };
    }

    public void Update(
        int serviceCategoryId,
        string? serviceName,
        string? description,
        string? specialInstruction,
        bool hasBranchPermission,
        bool allowBranchEditPrice,
        int modifiedBy)
    {
        ServiceCategoryId = serviceCategoryId;
        ServiceName = serviceName;
        Description = description;
        SpecialInstruction = specialInstruction;
        HasBranchPermission = hasBranchPermission;
        AllowBranchEditPrice = allowBranchEditPrice;

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
