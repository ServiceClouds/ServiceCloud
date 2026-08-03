using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Company")]
[Index("CompanyCode", Name = "IX_Company_CompanyCode")]
[Index("CountryId", Name = "IX_Company_CountryId")]
[Index("CurrencyId", Name = "IX_Company_CurrencyId")]
[Index("IsActive", Name = "IX_Company_IsActive")]
[Index("CompanyName", Name = "IX_Company_Name")]
public partial class Company
{
    [Key]
    public int CompanyId { get; set; }

    public int CountryId { get; set; }

    public int CurrencyId { get; set; }

    [StringLength(200)]
    public string? CompanyName { get; set; }

    [StringLength(50)]
    public string CompanyCode { get; set; } = null!;

    [Column("NTN")]
    [StringLength(100)]
    public string? Ntn { get; set; }

    [StringLength(100)]
    public string? RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Website { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Fax { get; set; }

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
    public string? PostalCode { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [Column("AppleStoreURL")]
    [StringLength(200)]
    [Unicode(false)]
    public string? AppleStoreUrl { get; set; }

    [Column("GooglePlayStoreURL")]
    [StringLength(200)]
    [Unicode(false)]
    public string? GooglePlayStoreUrl { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [ForeignKey("CountryId")]
    [InverseProperty("Companies")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("CurrencyId")]
    [InverseProperty("Companies")]
    public virtual Currency Currency { get; set; } = null!;

    [InverseProperty("Company")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
