using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Index("CardNumber", Name = "IX_Staff_CardNumber")]
[Index("CompanyId", Name = "IX_Staff_CompanyId")]
[Index("CountryId", Name = "IX_Staff_CountryId")]
[Index("Email", Name = "IX_Staff_Email")]
[Index("FirstName", Name = "IX_Staff_FirstName")]
[Index("AllowLogin", Name = "IX_Staff_IsActive")]
[Index("IsSuperAdmin", Name = "IX_Staff_IsSuperAdmin")]
[Index("LastName", Name = "IX_Staff_LastName")]
[Index("StaffPositionId", Name = "IX_Staff_StaffPositionId")]
[Index("StateCountryId", Name = "IX_Staff_StateCountryId")]
public partial class Staff
{
    [Key]
    public int StaffId { get; set; }

    public int CompanyId { get; set; }

    public int StaffPositionId { get; set; }

    public int? CountryId { get; set; }

    public int? StateCountryId { get; set; }

    public int? EnterpriseRoleId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? ProbationDurationTypeId { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    [StringLength(120)]
    public string FirstName { get; set; } = null!;

    [StringLength(60)]
    public string? LastName { get; set; }

    [StringLength(181)]
    public string? FullName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? CardNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(30)]
    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(500)]
    public string? AddressLine1 { get; set; }

    [StringLength(500)]
    public string? AddressLine2 { get; set; }

    [StringLength(100)]
    public string? CityName { get; set; }

    [StringLength(100)]
    public string? StateCountryName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PostCode { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public int? ProbationMonths { get; set; }

    public int? ProbationValue { get; set; }

    public int? OrganizationalDate { get; set; }

    public int? EmploymentType { get; set; }

    [StringLength(3000)]
    public string? Notes { get; set; }

    public bool AllowLogin { get; set; }

    public bool IsSuperAdmin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Staff")]
    public virtual Company Company { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Staff")]
    public virtual Country? Country { get; set; }

    [InverseProperty("Staff")]
    public virtual ICollection<StaffBranch> StaffBranches { get; set; } = new List<StaffBranch>();

    [ForeignKey("StaffPositionId")]
    [InverseProperty("Staff")]
    public virtual StaffPosition StaffPosition { get; set; } = null!;

    [ForeignKey("StateCountryId")]
    [InverseProperty("Staff")]
    public virtual StateCountry? StateCountry { get; set; }
}
