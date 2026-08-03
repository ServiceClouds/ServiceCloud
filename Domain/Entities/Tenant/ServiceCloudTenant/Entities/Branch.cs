using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Branch")]
[Index("BranchCode", Name = "IX_Branch_BranchCode")]
[Index("CompanyId", Name = "IX_Branch_CompanyId")]
[Index("CountryId", Name = "IX_Branch_CountryId")]
[Index("IsActive", Name = "IX_Branch_IsActive")]
[Index("IsOnline", Name = "IX_Branch_IsOnline")]
public partial class Branch
{
    [Key]
    public int BranchId { get; set; }

    public int CompanyId { get; set; }

    public int CountryId { get; set; }

    [StringLength(160)]
    public string? BranchName { get; set; }

    [StringLength(50)]
    public string BranchCode { get; set; } = null!;

    [StringLength(100)]
    public string? CityName { get; set; }

    [StringLength(100)]
    public string? StateCountryName { get; set; }

    [StringLength(500)]
    public string? AddressLine1 { get; set; }

    [StringLength(500)]
    public string? AddressLine2 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TimeZone { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Currency { get; set; }

    public int? DateFormatId { get; set; }

    [Column("TermsOfServiceURL")]
    [StringLength(250)]
    [Unicode(false)]
    public string? TermsOfServiceUrl { get; set; }

    [Column("PrivacyPolicyURL")]
    [StringLength(250)]
    [Unicode(false)]
    public string? PrivacyPolicyUrl { get; set; }

    public bool IsActive { get; set; }

    public bool? IsArchived { get; set; }

    public bool IsOnline { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Branches")]
    public virtual Company Company { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Branches")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Branch")]
    public virtual ICollection<StaffBranch> StaffBranches { get; set; } = new List<StaffBranch>();
}
