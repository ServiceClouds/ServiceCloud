using Domain.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Tenant.ServiceCloudTenant.Entities;


[Table("Country")]
public partial class Country
{
    private Country()
    {
    }

    [Key]
    public int CountryId { get; private set; }

    [StringLength(100)]
    public string CountryName { get; private set; } = null!;

    [StringLength(10)]
    [Column(TypeName = "varchar")]
    public string? CountryCode { get; private set; }

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    [InverseProperty("Country")]
    public virtual ICollection<Branch> Branches { get; private set; }
        = new List<Branch>();

    [InverseProperty("Country")]
    public virtual ICollection<Company> Companies { get; private set; }
        = new List<Company>();

    [InverseProperty("Country")]
    public virtual ICollection<Staff> Staff { get; private set; }
        = new List<Staff>();

    [InverseProperty("Country")]
    public virtual ICollection<StateCountry> StateCountries { get; private set; }
        = new List<StateCountry>();


    public static Country Create(
        string countryName,
        string? countryCode,
        int createdBy)
    {
        return new Country
        {
            CountryName = countryName,
            CountryCode = countryCode,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        string countryName,
        string? countryCode,
        int modifiedBy)
    {
        CountryName = countryName;
        CountryCode = countryCode;

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