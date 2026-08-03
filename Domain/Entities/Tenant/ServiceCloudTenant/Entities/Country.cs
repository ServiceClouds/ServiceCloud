using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Country")]
[Index("CountryCode", Name = "IX_Country_CountryCode")]
[Index("IsActive", Name = "IX_Country_IsActive")]
public partial class Country
{
    [Key]
    public int CountryId { get; set; }

    [StringLength(100)]
    public string CountryName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? CountryCode { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [InverseProperty("Country")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [InverseProperty("Country")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    [InverseProperty("Country")]
    public virtual ICollection<StateCountry> StateCountries { get; set; } = new List<StateCountry>();
}
