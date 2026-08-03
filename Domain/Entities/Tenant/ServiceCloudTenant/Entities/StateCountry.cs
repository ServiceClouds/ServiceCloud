using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StateCountry")]
[Index("CountryId", Name = "IX_StateCountry_CountryId")]
[Index("IsActive", Name = "IX_StateCountry_IsActive")]
public partial class StateCountry
{
    [Key]
    public int StateCountryId { get; set; }

    [StringLength(100)]
    public string StateCountryName { get; set; } = null!;

    public int CountryId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("StateCountries")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("StateCountry")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
